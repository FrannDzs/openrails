﻿// COPYRIGHT 2013 - 2021 by the Open Rails project.
// 
// This file is part of Open Rails.
// 
// Open Rails is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Open Rails is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Open Rails.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Orts.Formats.Msts;
using Orts.Parsers.Msts;
using Orts.Simulation.RollingStocks.SubSystems.Controllers;
using ORTS.Common;
using ORTS.Scripting.Api;
namespace Orts.Simulation.RollingStocks.SubSystems
{
    public class CruiseControl

    {
        MSTSLocomotive Locomotive;
        readonly Simulator Simulator;
        public bool SpeedIsMph = false;
        public bool SpeedRegulatorMaxForcePercentUnits = false;
        public float SpeedRegulatorMaxForceSteps = 0;
        public bool MaxForceSetSingleStep = false;
        public bool MaxForceKeepSelectedStepWhenManualModeSet = false;
        public bool KeepSelectedSpeedWhenManualModeSet = false;
        public bool ForceRegulatorAutoWhenNonZeroSpeedSelected = false;
        public bool ForceRegulatorAutoWhenNonZeroForceSelected = false;
        public bool ForceRegulatorAutoWhenNonZeroSpeedSelectedAndThrottleAtZero = false;
        public bool MaxForceSelectorIsDiscrete = false;
        public List<string> SpeedRegulatorOptions = new List<string>();
        public SpeedRegulatorMode SpeedRegMode = SpeedRegulatorMode.Manual;
        public SpeedSelectorMode SpeedSelMode = SpeedSelectorMode.Neutral;
        public ControllerCruiseControlLogic CruiseControlLogic = new ControllerCruiseControlLogic();
        public float SelectedMaxAccelerationPercent = 0;
        public float SelectedMaxAccelerationStep = 0;
        public float SelectedSpeedMpS = 0;
        public float SetSpeedMpS
        {
            get
            {
                float selected = RestrictedRegionOdometer.Started ? CurrentSelectedSpeedMpS : SelectedSpeedMpS;
                return selected;
            }
        }
        public int SelectedNumberOfAxles = 0;
        public float SpeedRegulatorNominalSpeedStepMpS = 0;
        public float SpeedRegulatorNominalSpeedStepKpHOrMpH = 0;
        public float MaxAccelerationMpSS = 1;
        public float MaxDecelerationMpSS = 0.5f;
        public bool UseThrottleInCombinedControl = false;
        public bool AntiWheelSpinEquipped = false;
        public float AntiWheelSpinSpeedDiffThreshold = 0.5f;
        public float DynamicBrakeMaxForceAtSelectorStep = 0;
        protected float trainBrakePercent = 0;
        protected float trainLength = 0;
        public int TrainLengthMeters = 0;
        public float CurrentSelectedSpeedMpS = 0;
        OdoMeter RestrictedRegionOdometer;
        public float StartReducingSpeedDelta = 0.5f;
        public float StartReducingSpeedDeltaDownwards = 0f;
        public bool DynamicBrakePriority = false;
        public List<int> ForceStepsThrottleTable = new List<int>();
        public List<float> AccelerationTable = new List<float>();
        public enum SpeedRegulatorMode { Manual, Auto, Testing, AVV }
        public enum SpeedSelectorMode { Parking, Neutral, On, Start }
        public float DynamicBrakeIncreaseSpeed = 0;
        public float DynamicBrakeDecreaseSpeed = 0;
        public uint MinimumMetersToPass = 19;
        public float AccelerationRampMaxMpSSS = 0.7f;
        public float AccelerationDemandMpSS;
        public float AccelerationRampMinMpSSS = 0.01f;
        public bool ResetForceAfterAnyBraking = false;
        public float ThrottleFullRangeIncreaseTimeSeconds = 6;
        public float ThrottleFullRangeDecreaseTimeSeconds = 6;
        public float DynamicBrakeFullRangeIncreaseTimeSeconds;
        public float DynamicBrakeFullRangeDecreaseTimeSeconds;
        public float TrainBrakeFullRangeIncreaseTimeSeconds = 10;
        public float TrainBrakeFullRangeDecreaseTimeSeconds = 5;
        public float ParkingBrakeEngageSpeed = 0;
        public float ParkingBrakePercent = 0;
        public bool SkipThrottleDisplay = false;
        public bool DisableZeroForceStep = false;
        public bool DynamicBrakeIsSelectedForceDependant = false;
        public bool UseThrottleAsSpeedSelector = false;
        public bool UseThrottleAsForceSelector = false;
        public bool ContinuousSpeedIncreasing = false;
        public bool ContinuousSpeedDecreasing = false;
        public float PowerBreakoutAmpers = 0;
        public float PowerBreakoutSpeedDelta = 0;
        public float PowerResumeSpeedDelta = 0;
        public float PowerReductionDelayPaxTrain = 0;
        public float PowerReductionDelayCargoTrain = 0;
        public float PowerReductionValue = 0;
        public float MaxPowerThreshold = 0;
        public float SafeSpeedForAutomaticOperationMpS = 0;
        protected float SpeedSelectorStepTimeSeconds = 0;
        protected float TotalTime = 0;
        public bool DisableCruiseControlOnThrottleAndZeroSpeed = false;
        public bool DisableCruiseControlOnThrottleAndZeroForce = false;
        public bool DisableCruiseControlOnThrottleAndZeroForceAndZeroSpeed = false;
        public bool ForceResetRequiredAfterBraking = false;
        public bool ForceResetIncludeDynamicBrake = false;
        public bool ZeroSelectedSpeedWhenPassingToThrottleMode = false;
        public bool DynamicBrakeCommandHasPriorityOverCruiseControl = true;
        public bool HasIndependentThrottleDynamicBrakeLever = false;
        public bool HasProportionalSpeedSelector = false;
        public bool SpeedSelectorIsDiscrete = false;
        public bool DoComputeNumberOfAxles = false;
        public bool DisableManualSwitchToManualWhenSetForceNotAtZero = false;
        public bool DisableManualSwitchToAutoWhenThrottleNotAtZero = false;
        public bool DisableManualSwitchToAutoWhenSetSpeedNotAtTop = false;
        public bool EnableSelectedSpeedSelectionWhenManualModeSet = false;
        public bool UseTrainBrakeAndDynBrake = false;
        protected float SpeedDeltaToEnableTrainBrake = 5;
        protected float SpeedDeltaToEnableFullTrainBrake = 10;
        public float MinimumSpeedForCCEffectMpS = 0;
        protected float speedRegulatorIntermediateValue = 0;
        protected float StepSize = 20;
        protected float RelativeAccelerationMpSS => Locomotive.Direction == Direction.Reverse ? -Locomotive.AccelerationMpSS : Locomotive.AccelerationMpSS; // Acceleration relative to state of reverser
        public bool CCIsUsingTrainBrake = false; // Cruise control is using (also) train brake to brake
        protected float TrainBrakeMinPercentValue = 30f; // Minimum train brake settable percent Value
        protected float TrainBrakeMaxPercentValue = 85f; // Maximum train brake settable percent Value
        public bool StartInAutoMode = false; // at startup cruise control is in auto mode
        public bool ThrottleNeutralPosition = false; // when UseThrottleAsSpeedSelector is true and this is true
                                                     // and we are in auto mode, the throttle zero position is a neutral position
        public bool ThrottleLowSpeedPosition = false; // when UseThrottleAsSpeedSelector is true and this is true
                                                      // and we are in auto mode, the first throttle above zero position is used to run at low speed
        public float LowSpeed = 2f; // default parking speed
        public bool HasTwoForceValues = false; // when UseThrottleAsSpeedSelector is true, two max force values (50% and 100%) are available

        protected bool firstIteration = true;
        // CCThrottleOrDynBrakePercent may vary from -100 to 100 and is the percentage value which the Cruise Control
        // sets to throttle (if CCThrottleOrDynBrakePercent >=0) or to dynamic brake (if CCThrottleOrDynBrakePercent <0)
        public float CCThrottleOrDynBrakePercent = 0;
        protected bool breakout = false;
        protected float timeFromEngineMoved = 0;
        protected bool reducingForce = false;
        protected float skidSpeedDegratation = 0;
        public bool TrainBrakePriority = false;
        public bool WasBraking = false;
        public bool WasForceReset = true;


        public bool SelectingSpeedPressed = false;
        public bool EngineBrakePriority = false;
        public int AccelerationBits = 0;
        public bool Speed0Pressed, Speed10Pressed, Speed20Pressed, Speed30Pressed, Speed40Pressed, Speed50Pressed
            , Speed60Pressed, Speed70Pressed, Speed80Pressed, Speed90Pressed, Speed100Pressed
            , Speed110Pressed, Speed120Pressed, Speed130Pressed, Speed140Pressed, Speed150Pressed
            , Speed160Pressed, Speed170Pressed, Speed180Pressed, Speed190Pressed, Speed200Pressed;

        public CruiseControl(MSTSLocomotive locomotive)
        {
            Locomotive = locomotive;
            Simulator = Locomotive.Simulator;

            RestrictedRegionOdometer = new OdoMeter(locomotive);
        }

        public CruiseControl(CruiseControl other, MSTSLocomotive locomotive)
        {
            Simulator = locomotive.Simulator;
            Locomotive = locomotive;
            
            RestrictedRegionOdometer = new OdoMeter(locomotive);

            SpeedIsMph = other.SpeedIsMph;
            SpeedRegulatorMaxForcePercentUnits = other.SpeedRegulatorMaxForcePercentUnits;
            SpeedRegulatorMaxForceSteps = other.SpeedRegulatorMaxForceSteps;
            MaxForceSetSingleStep = other.MaxForceSetSingleStep;
            MaxForceKeepSelectedStepWhenManualModeSet = other.MaxForceKeepSelectedStepWhenManualModeSet;
            KeepSelectedSpeedWhenManualModeSet = other.KeepSelectedSpeedWhenManualModeSet;
            ForceRegulatorAutoWhenNonZeroSpeedSelected = other.ForceRegulatorAutoWhenNonZeroSpeedSelected;
            ForceRegulatorAutoWhenNonZeroForceSelected = other.ForceRegulatorAutoWhenNonZeroForceSelected;
            ForceRegulatorAutoWhenNonZeroSpeedSelectedAndThrottleAtZero = other.ForceRegulatorAutoWhenNonZeroSpeedSelectedAndThrottleAtZero;
            MaxForceSelectorIsDiscrete = other.MaxForceSelectorIsDiscrete;
            SpeedRegulatorOptions = other.SpeedRegulatorOptions;
            CruiseControlLogic = other.CruiseControlLogic;
            SpeedRegulatorNominalSpeedStepMpS = other.SpeedRegulatorNominalSpeedStepMpS;
            SpeedRegulatorNominalSpeedStepKpHOrMpH = other.SpeedRegulatorNominalSpeedStepKpHOrMpH;
            MaxAccelerationMpSS = other.MaxAccelerationMpSS;
            MaxDecelerationMpSS = other.MaxDecelerationMpSS;
            UseThrottleInCombinedControl = other.UseThrottleInCombinedControl;
            AntiWheelSpinEquipped = other.AntiWheelSpinEquipped;
            AntiWheelSpinSpeedDiffThreshold = other.AntiWheelSpinSpeedDiffThreshold;
            DynamicBrakeMaxForceAtSelectorStep = other.DynamicBrakeMaxForceAtSelectorStep;
            StartReducingSpeedDelta = other.StartReducingSpeedDelta;
            StartReducingSpeedDeltaDownwards = other.StartReducingSpeedDeltaDownwards;
            ForceStepsThrottleTable = other.ForceStepsThrottleTable;
            AccelerationTable = other.AccelerationTable;
            DynamicBrakeIncreaseSpeed = other.DynamicBrakeIncreaseSpeed;
            DynamicBrakeDecreaseSpeed = other.DynamicBrakeDecreaseSpeed;
            AccelerationRampMaxMpSSS = other.AccelerationRampMaxMpSSS;
            AccelerationRampMinMpSSS = other.AccelerationRampMinMpSSS;
            ResetForceAfterAnyBraking = other.ResetForceAfterAnyBraking;
            ThrottleFullRangeIncreaseTimeSeconds = other.ThrottleFullRangeIncreaseTimeSeconds;
            ThrottleFullRangeDecreaseTimeSeconds = other.ThrottleFullRangeDecreaseTimeSeconds;
            DynamicBrakeFullRangeIncreaseTimeSeconds = other.DynamicBrakeFullRangeIncreaseTimeSeconds;
            DynamicBrakeFullRangeDecreaseTimeSeconds = other.DynamicBrakeFullRangeDecreaseTimeSeconds;
            TrainBrakeFullRangeIncreaseTimeSeconds = other.TrainBrakeFullRangeIncreaseTimeSeconds;
            TrainBrakeFullRangeDecreaseTimeSeconds = other.TrainBrakeFullRangeDecreaseTimeSeconds;
            ParkingBrakeEngageSpeed = other.ParkingBrakeEngageSpeed;
            ParkingBrakePercent = other.ParkingBrakePercent;
            DisableZeroForceStep = other.DisableZeroForceStep;
            DynamicBrakeIsSelectedForceDependant = other.DynamicBrakeIsSelectedForceDependant;
            UseThrottleAsSpeedSelector = other.UseThrottleAsSpeedSelector;
            UseThrottleAsForceSelector = other.UseThrottleAsForceSelector;
            ContinuousSpeedIncreasing = other.ContinuousSpeedIncreasing;
            ContinuousSpeedDecreasing = other.ContinuousSpeedDecreasing;
            PowerBreakoutAmpers = other.PowerBreakoutAmpers;
            PowerBreakoutSpeedDelta = other.PowerBreakoutSpeedDelta;
            PowerResumeSpeedDelta = other.PowerResumeSpeedDelta;
            PowerReductionDelayPaxTrain = other.PowerReductionDelayPaxTrain;
            PowerReductionDelayCargoTrain = other.PowerReductionDelayCargoTrain;
            PowerReductionValue = other.PowerReductionValue;
            MaxPowerThreshold = other.MaxPowerThreshold;
            SafeSpeedForAutomaticOperationMpS = other.SafeSpeedForAutomaticOperationMpS;
            SpeedSelectorStepTimeSeconds = other.SpeedSelectorStepTimeSeconds;
            DisableCruiseControlOnThrottleAndZeroSpeed = other.DisableCruiseControlOnThrottleAndZeroSpeed;
            DisableCruiseControlOnThrottleAndZeroForce = other.DisableCruiseControlOnThrottleAndZeroForce;
            DisableCruiseControlOnThrottleAndZeroForceAndZeroSpeed = other.DisableCruiseControlOnThrottleAndZeroForceAndZeroSpeed;
            ForceResetRequiredAfterBraking = other.ForceResetRequiredAfterBraking;
            ForceResetIncludeDynamicBrake = other.ForceResetIncludeDynamicBrake;
            ZeroSelectedSpeedWhenPassingToThrottleMode = other.ZeroSelectedSpeedWhenPassingToThrottleMode;
            DynamicBrakeCommandHasPriorityOverCruiseControl = other.DynamicBrakeCommandHasPriorityOverCruiseControl;
            HasIndependentThrottleDynamicBrakeLever = other.HasIndependentThrottleDynamicBrakeLever;
            HasProportionalSpeedSelector = other.HasProportionalSpeedSelector;
            DisableManualSwitchToManualWhenSetForceNotAtZero = other.DisableManualSwitchToManualWhenSetForceNotAtZero;
            DisableManualSwitchToAutoWhenThrottleNotAtZero = other.DisableManualSwitchToAutoWhenThrottleNotAtZero;
            DisableManualSwitchToAutoWhenSetSpeedNotAtTop = other.DisableManualSwitchToAutoWhenSetSpeedNotAtTop;
            EnableSelectedSpeedSelectionWhenManualModeSet = other.EnableSelectedSpeedSelectionWhenManualModeSet;
            SpeedSelectorIsDiscrete = other.SpeedSelectorIsDiscrete;
            DoComputeNumberOfAxles = other.DoComputeNumberOfAxles;
            UseTrainBrakeAndDynBrake = other.UseTrainBrakeAndDynBrake;
            SpeedDeltaToEnableTrainBrake = other.SpeedDeltaToEnableTrainBrake;
            SpeedDeltaToEnableFullTrainBrake = other.SpeedDeltaToEnableFullTrainBrake;
            MinimumSpeedForCCEffectMpS = other.MinimumSpeedForCCEffectMpS;
            TrainBrakeMinPercentValue = other.TrainBrakeMinPercentValue;
            TrainBrakeMaxPercentValue = other.TrainBrakeMaxPercentValue;
            StartInAutoMode = other.StartInAutoMode;
            ThrottleNeutralPosition = other.ThrottleNeutralPosition;
            ThrottleLowSpeedPosition = other.ThrottleLowSpeedPosition;
            LowSpeed = other.LowSpeed;
            HasTwoForceValues = other.HasTwoForceValues;

        }

        public void Parse(STFReader stf)
        {
            stf.MustMatch("(");
            while (!stf.EndOfBlock())
            {
                switch (stf.ReadItem().ToLower())
                {
                    case "speedismph": SpeedIsMph = stf.ReadBoolBlock(false); break;
                    case "usethrottleincombinedcontrol": UseThrottleInCombinedControl = stf.ReadBoolBlock(false); break;
                    case "speedselectorsteptimeseconds": SpeedSelectorStepTimeSeconds = stf.ReadFloatBlock(STFReader.UNITS.Any, 0.1f); break;
                    case "throttlefullrangeincreasetimeseconds": ThrottleFullRangeIncreaseTimeSeconds = stf.ReadFloatBlock(STFReader.UNITS.Any, 5); break;
                    case "throttlefullrangedecreasetimeseconds": ThrottleFullRangeDecreaseTimeSeconds = stf.ReadFloatBlock(STFReader.UNITS.Any, 5); break;
                    case "resetforceafteranybraking": ResetForceAfterAnyBraking = stf.ReadBoolBlock(false); break;
                    case "dynamicbrakefullrangeincreasetimeseconds": DynamicBrakeFullRangeIncreaseTimeSeconds = stf.ReadFloatBlock(STFReader.UNITS.Any, 5); break;
                    case "dynamicbrakefullrangedecreasetimeseconds": DynamicBrakeFullRangeDecreaseTimeSeconds = stf.ReadFloatBlock(STFReader.UNITS.Any, 5); break;
                    case "trainbrakefullrangeincreasetimeseconds": TrainBrakeFullRangeIncreaseTimeSeconds = stf.ReadFloatBlock(STFReader.UNITS.Any, 10); break;
                    case "trainbrakefullrangedecreasetimeseconds": TrainBrakeFullRangeDecreaseTimeSeconds = stf.ReadFloatBlock(STFReader.UNITS.Any, 5); break;
                    case "parkingbrakeengagespeed": ParkingBrakeEngageSpeed = stf.ReadFloatBlock(STFReader.UNITS.Speed, 0); break;
                    case "parkingbrakepercent": ParkingBrakePercent = stf.ReadFloatBlock(STFReader.UNITS.Any, 0); break;
                    case "maxpowerthreshold": MaxPowerThreshold = stf.ReadFloatBlock(STFReader.UNITS.Any, 0); break;
                    case "safespeedforautomaticoperationmps": SafeSpeedForAutomaticOperationMpS = stf.ReadFloatBlock(STFReader.UNITS.Any, 0); break;
                    case "maxforcepercentunits": SpeedRegulatorMaxForcePercentUnits = stf.ReadBoolBlock(false); break;
                    case "maxforcesteps": SpeedRegulatorMaxForceSteps = stf.ReadIntBlock(0); break;
                    case "maxforcesetsinglestep": MaxForceSetSingleStep = stf.ReadBoolBlock(false); break;
                    case "maxforcekeepselectedstepwhenmanualmodeset": MaxForceKeepSelectedStepWhenManualModeSet = stf.ReadBoolBlock(false); break;
                    case "keepselectedspeedwhenmanualmodeset": KeepSelectedSpeedWhenManualModeSet = stf.ReadBoolBlock(false); break;
                    case "forceregulatorautowhennonzerospeedselected": ForceRegulatorAutoWhenNonZeroSpeedSelected = stf.ReadBoolBlock(false); break;
                    case "forceregulatorautowhennonzeroforceselected": ForceRegulatorAutoWhenNonZeroForceSelected = stf.ReadBoolBlock(false); break;
                    case "forceregulatorautowhennonzerospeedselectedandthrottleatzero": ForceRegulatorAutoWhenNonZeroSpeedSelectedAndThrottleAtZero = stf.ReadBoolBlock(false); break;
                    case "maxforceselectorisdiscrete": MaxForceSelectorIsDiscrete = stf.ReadBoolBlock(false); break;
                    case "continuousspeedincreasing": ContinuousSpeedIncreasing = stf.ReadBoolBlock(false); break;
                    case "disablecruisecontrolonthrottleandzerospeed": DisableCruiseControlOnThrottleAndZeroSpeed = stf.ReadBoolBlock(false); break;
                    case "disablecruisecontrolonthrottleandzeroforce": DisableCruiseControlOnThrottleAndZeroForce = stf.ReadBoolBlock(false); break;
                    case "disablecruisecontrolonthrottleandzeroforceandzerospeed": DisableCruiseControlOnThrottleAndZeroForceAndZeroSpeed = stf.ReadBoolBlock(false); break;
                    case "disablemanualswitchtomanualwhensetforcenotatzero": DisableManualSwitchToManualWhenSetForceNotAtZero = stf.ReadBoolBlock(false); break;
                    case "disablemanualswitchtoautowhenthrottlenotatzero": DisableManualSwitchToAutoWhenThrottleNotAtZero = stf.ReadBoolBlock(false); break;
                    case "disablemanualswitchtoautowhensetspeednotattop": DisableManualSwitchToAutoWhenSetSpeedNotAtTop = stf.ReadBoolBlock(false); break;
                    case "enableselectedspeedselectionwhenmanualmodeset": EnableSelectedSpeedSelectionWhenManualModeSet = stf.ReadBoolBlock(false); break;
                    case "forcestepsthrottletable":
                        foreach (var forceStepThrottleValue in stf.ReadStringBlock("").Replace(" ", "").Split(','))
                        {
                            ForceStepsThrottleTable.Add(int.Parse(forceStepThrottleValue));
                        }
                        break;
                    case "accelerationtable":
                        foreach (var accelerationValue in stf.ReadStringBlock("").Replace(" ", "").Split(','))
                        {
                            AccelerationTable.Add(float.Parse(accelerationValue));
                        }
                        break;
                    case "powerbreakoutampers": PowerBreakoutAmpers = stf.ReadFloatBlock(STFReader.UNITS.Any, 100.0f); break;
                    case "powerbreakoutspeeddelta": PowerBreakoutSpeedDelta = stf.ReadFloatBlock(STFReader.UNITS.Any, 100.0f); break;
                    case "powerresumespeeddelta": PowerResumeSpeedDelta = stf.ReadFloatBlock(STFReader.UNITS.Any, 100.0f); break;
                    case "powerreductiondelaypaxtrain": PowerReductionDelayPaxTrain = stf.ReadFloatBlock(STFReader.UNITS.Any, 0.0f); break;
                    case "powerreductiondelaycargotrain": PowerReductionDelayCargoTrain = stf.ReadFloatBlock(STFReader.UNITS.Any, 0.0f); break;
                    case "powerreductionvalue": PowerReductionValue = stf.ReadFloatBlock(STFReader.UNITS.Any, 100.0f); break;
                    case "disablezeroforcestep": DisableZeroForceStep = stf.ReadBoolBlock(false); break;
                    case "dynamicbrakeisselectedforcedependant": DynamicBrakeIsSelectedForceDependant = stf.ReadBoolBlock(false); break;
                    case "defaultforcestep": SelectedMaxAccelerationStep = stf.ReadFloatBlock(STFReader.UNITS.Any, 1.0f); break;
                    case "dynamicbrakemaxforceatselectorstep": DynamicBrakeMaxForceAtSelectorStep = stf.ReadFloatBlock(STFReader.UNITS.Any, 1.0f); break;
                    case "startreducingspeeddelta": StartReducingSpeedDelta = (stf.ReadFloatBlock(STFReader.UNITS.Any, 1.0f) / 10); break;
                    case "startreducingspeeddeltadownwards": StartReducingSpeedDeltaDownwards = (stf.ReadFloatBlock(STFReader.UNITS.Any, 1.0f) / 10); break;
                    case "maxacceleration": MaxAccelerationMpSS = stf.ReadFloatBlock(STFReader.UNITS.Any, 1); break;
                    case "maxdeceleration": MaxDecelerationMpSS = stf.ReadFloatBlock(STFReader.UNITS.Any, 0.5f); break;
                    case "antiwheelspinequipped": AntiWheelSpinEquipped = stf.ReadBoolBlock(false); break;
                    case "antiwheelspinspeeddiffthreshold": AntiWheelSpinSpeedDiffThreshold = stf.ReadFloatBlock(STFReader.UNITS.None, 0.5f); break;
                    case "nominalspeedstep":
                        {
                            SpeedRegulatorNominalSpeedStepKpHOrMpH = stf.ReadFloatBlock(STFReader.UNITS.Speed, 0);
                            SpeedRegulatorNominalSpeedStepMpS = SpeedIsMph ? MpS.FromMpH(SpeedRegulatorNominalSpeedStepKpHOrMpH) : MpS.FromKpH(SpeedRegulatorNominalSpeedStepKpHOrMpH);
                            break;
                        }
                    case "usethrottleasspeedselector": UseThrottleAsSpeedSelector = stf.ReadBoolBlock(false); break;
                    case "usethrottleasforceselector": UseThrottleAsForceSelector = stf.ReadBoolBlock(false); break;
                    case "dynamicbrakeincreasespeed": DynamicBrakeIncreaseSpeed = stf.ReadFloatBlock(STFReader.UNITS.Any, 0.5f); break;
                    case "dynamicbrakedecreasespeed": DynamicBrakeDecreaseSpeed = stf.ReadFloatBlock(STFReader.UNITS.Any, 0.5f); break;
                    case "forceresetrequiredafterbraking": ForceResetRequiredAfterBraking = stf.ReadBoolBlock(false); break;
                    case "forceresetincludedynamicbrake": ForceResetIncludeDynamicBrake = stf.ReadBoolBlock(false); break;
                    case "zeroselectedspeedwhenpassingtothrottlemode": ZeroSelectedSpeedWhenPassingToThrottleMode = stf.ReadBoolBlock(false); break;
                    case "dynamicbrakecommandhaspriorityovercruisecontrol": DynamicBrakeCommandHasPriorityOverCruiseControl = stf.ReadBoolBlock(true); break;
                    case "hasindependentthrottledynamicbrakelever": HasIndependentThrottleDynamicBrakeLever = stf.ReadBoolBlock(false); break;
                    case "hasproportionalspeedselector": HasProportionalSpeedSelector = stf.ReadBoolBlock(false); break;
                    case "speedselectorisdiscrete": SpeedSelectorIsDiscrete = stf.ReadBoolBlock(false); break;
                    case "usetrainbrakeanddynbrake": UseTrainBrakeAndDynBrake = stf.ReadBoolBlock(false); break;
                    case "speeddeltatoenabletrainbrake": SpeedDeltaToEnableTrainBrake = stf.ReadFloatBlock(STFReader.UNITS.Speed, 5f); break;
                    case "speeddeltatoenablefulltrainbrake": SpeedDeltaToEnableFullTrainBrake = stf.ReadFloatBlock(STFReader.UNITS.Speed, 10f); break;
                    case "minimumspeedforcceffect": MinimumSpeedForCCEffectMpS = stf.ReadFloatBlock(STFReader.UNITS.Speed, 0f); break;
                    case "trainbrakeminpercentvalue": TrainBrakeMinPercentValue = stf.ReadFloatBlock(STFReader.UNITS.Any, 0.3f); break;
                    case "trainbrakemaxpercentvalue": TrainBrakeMaxPercentValue = stf.ReadFloatBlock(STFReader.UNITS.Any, 0.85f); break;
                    case "startinautomode": StartInAutoMode = stf.ReadBoolBlock(false); break;
                    case "throttleneutralposition": ThrottleNeutralPosition = stf.ReadBoolBlock(false); break;
                    case "throttlelowspeedposition": ThrottleLowSpeedPosition = stf.ReadBoolBlock(false); break;
                    case "lowspeed": LowSpeed = stf.ReadFloatBlock(STFReader.UNITS.Speed, 2f); break;
                    case "hasttwoforcevalues": HasTwoForceValues = stf.ReadBoolBlock(false); break;
                    case "docomputenumberofaxles": DoComputeNumberOfAxles = stf.ReadBoolBlock(false); break;
                    case "options":
                        foreach (var speedRegulatorOption in stf.ReadStringBlock("").ToLower().Replace(" ", "").Split(','))
                        {
                            SpeedRegulatorOptions.Add(speedRegulatorOption.ToLower());
                        }
                        break;
                    case "controllercruisecontrollogic":
                    {
                        String speedControlLogic = stf.ReadStringBlock("none").ToLower();
                        switch (speedControlLogic)
                        {
                            case "full":
                                {
                                    CruiseControlLogic = ControllerCruiseControlLogic.Full;
                                    break;
                                }
                            case "speedonly":
                                {
                                    CruiseControlLogic = ControllerCruiseControlLogic.SpeedOnly;
                                    break;
                                }
                        }
                        break;
                    }
                    case "(": stf.SkipRestOfBlock(); break;
                    default: break;
                }
            }
        }

        public CruiseControl Clone(MSTSLocomotive locomotive)
        {
            return new CruiseControl(this, locomotive);
        }

        public void Initialize()
        {
            if (DynamicBrakeFullRangeIncreaseTimeSeconds == 0)
                DynamicBrakeFullRangeIncreaseTimeSeconds = 4;
            if (DynamicBrakeFullRangeDecreaseTimeSeconds == 0)
                DynamicBrakeFullRangeDecreaseTimeSeconds = 6;
            
            ComputeNumberOfAxles();
            if (StartReducingSpeedDeltaDownwards == 0) StartReducingSpeedDeltaDownwards = StartReducingSpeedDelta;
            if (StartInAutoMode) SpeedRegMode = SpeedRegulatorMode.Auto;
        }

        private void ComputeNumberOfAxles()
        {
            if (DoComputeNumberOfAxles && (TrainCar)Locomotive == Simulator.PlayerLocomotive)
            {
                SelectedNumberOfAxles = 0;
                foreach (TrainCar tc in Locomotive.Train.Cars)
                {
                    SelectedNumberOfAxles += tc.WheelAxles.Count;
                }
            }
        }

        bool IsActive = false;
        public void Update(float elapsedClockSeconds)
        {
            if (!Locomotive.IsPlayerTrain || Locomotive != Locomotive.Train.LeadLocomotive)
            {
                WasForceReset = false;
                CCThrottleOrDynBrakePercent = 0;
                trainBrakePercent = 0;
                return;
            }

            UpdateSelectedSpeed(elapsedClockSeconds);

            if (RestrictedRegionOdometer.Triggered)
            {
                RestrictedRegionOdometer.Stop();
                Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetString("Speed restricted zone off."));
                Locomotive.SignalEvent(Common.Event.CruiseControlAlert);
            }

            bool wasActive = IsActive;
            IsActive = false;

            if (SpeedRegMode != SpeedRegulatorMode.Auto || Locomotive.DynamicBrakePercent < 0)
                DynamicBrakePriority = false;

            if (Locomotive.TrainBrakeController.TCSEmergencyBraking || Locomotive.TrainBrakeController.TCSFullServiceBraking)
            {
                WasBraking = true;
                CCThrottleOrDynBrakePercent = 0;
                trainBrakePercent = 0;
            }
            else if (SpeedRegMode == SpeedRegulatorMode.Manual || (SpeedRegMode == SpeedRegulatorMode.Auto && DynamicBrakePriority))
            {
                WasForceReset = false;
                CCThrottleOrDynBrakePercent = 0;
                trainBrakePercent = 0;
            }
            else if (SpeedRegMode == SpeedRegulatorMode.Auto)
            {
                float prevTrainBrakePercent = trainBrakePercent;
                CalculateRequiredForce(elapsedClockSeconds, Locomotive.AbsWheelSpeedMpS);
                CCThrottleOrDynBrakePercent = MathHelper.Clamp(CCThrottleOrDynBrakePercent, -100, 100);
                if (CCThrottleOrDynBrakePercent >= 0)
                {
                    Locomotive.ThrottlePercent = CCThrottleOrDynBrakePercent;
                    Locomotive.DynamicBrakePercent = -1;
                }
                else
                {
                    if (Locomotive.DynamicBrakePercent < 0) Locomotive.DynamicBrakeCommandStartTime = Locomotive.Simulator.ClockTime;
                    Locomotive.ThrottlePercent = 0;
                    Locomotive.DynamicBrakePercent = -CCThrottleOrDynBrakePercent;
                }
                if (prevTrainBrakePercent != trainBrakePercent && !TrainBrakePriority)
                {
                    Locomotive.TrainBrakeController.SetPercent(trainBrakePercent); // TODO: do not move actual train brake lever
                }
                IsActive = true;
            }
            if (!IsActive && wasActive && SpeedRegMode == SpeedRegulatorMode.Auto)
            {
                Locomotive.ThrottlePercent = 0;
                if (!DynamicBrakePriority) Locomotive.DynamicBrakePercent = -1;
            }

            if (SpeedRegMode == SpeedRegulatorMode.Manual)
                SkipThrottleDisplay = false;

            if (maxForceIncreasing) SpeedRegulatorMaxForceIncrease(elapsedClockSeconds);
            if (maxForceIncreasing) SpeedRegulatorMaxForceIncrease(elapsedClockSeconds);
            if (maxForceDecreasing)
            {
                if (SelectedMaxAccelerationStep <= 0) maxForceDecreasing = false;
                else SpeedRegulatorMaxForceDecrease(elapsedClockSeconds);
            }
        }

        public void Save(BinaryWriter outf)
        {
            outf.Write(CurrentSelectedSpeedMpS);
            outf.Write(maxForceDecreasing);
            outf.Write(maxForceIncreasing);
            outf.Write(RestrictedRegionOdometer.Started);
            outf.Write(RestrictedRegionOdometer.RemainingValue);
            outf.Write(SelectedMaxAccelerationPercent);
            outf.Write(SelectedMaxAccelerationStep);
            outf.Write(SelectedNumberOfAxles);
            outf.Write(SelectedSpeedMpS);
            outf.Write(DynamicBrakePriority);
            outf.Write((int)SpeedRegMode);
            outf.Write((int)SpeedSelMode);
            outf.Write(trainBrakePercent);
            outf.Write(TrainLengthMeters);
            outf.Write(speedRegulatorIntermediateValue);
            outf.Write(CCIsUsingTrainBrake);
        }

        public void Restore(BinaryReader inf)
        {
            CurrentSelectedSpeedMpS = inf.ReadSingle();
            maxForceDecreasing = inf.ReadBoolean();
            maxForceIncreasing = inf.ReadBoolean();
            bool started = inf.ReadBoolean();
            RestrictedRegionOdometer.Setup(inf.ReadSingle());
            if (started) RestrictedRegionOdometer.Start();
            SelectedMaxAccelerationPercent = inf.ReadSingle();
            SelectedMaxAccelerationStep = inf.ReadSingle();
            SelectedNumberOfAxles = inf.ReadInt32();
            SelectedSpeedMpS = inf.ReadSingle();
            DynamicBrakePriority = inf.ReadBoolean();
            SpeedRegMode = (SpeedRegulatorMode)inf.ReadInt32();
            SpeedSelMode = (SpeedSelectorMode)inf.ReadInt32();
            trainBrakePercent = inf.ReadSingle();
            TrainLengthMeters = inf.ReadInt32();
            speedRegulatorIntermediateValue = inf.ReadSingle();
            CCIsUsingTrainBrake = inf.ReadBoolean();
        }

        public void UpdateSelectedSpeed(float elapsedClockSeconds)
        {
            TotalTime += elapsedClockSeconds;
            if (SpeedRegMode == CruiseControl.SpeedRegulatorMode.Auto && !DynamicBrakePriority ||
             EnableSelectedSpeedSelectionWhenManualModeSet)
            {
                if (selectedSpeedIncreasing) SpeedRegulatorSelectedSpeedIncrease();
                if (SelectedSpeedDecreasing) SpeedRegulatorSelectedSpeedDecrease();
            }
        }


        public void SpeedRegulatorModeIncrease()
        {
            if (!Locomotive.IsPlayerTrain) return;
            Locomotive.SignalEvent(Common.Event.CruiseControlSpeedRegulator);
            SpeedRegulatorMode previousMode = SpeedRegMode;
            if (SpeedRegMode == SpeedRegulatorMode.Testing) return;
            if (SpeedRegMode == SpeedRegulatorMode.Manual &&
               ((DisableManualSwitchToAutoWhenThrottleNotAtZero && (Locomotive.ThrottlePercent != 0 ||
               (Locomotive.DynamicBrakePercent != -1 && Locomotive.DynamicBrakePercent != 0))) ||
               (DisableManualSwitchToAutoWhenSetSpeedNotAtTop && SelectedSpeedMpS != Locomotive.MaxSpeedMpS && Locomotive.AbsSpeedMpS > Simulator.MaxStoppedMpS)))
                return;
            bool test = false;
            while (!test)
            {
                SpeedRegMode++;
                switch (SpeedRegMode)
                {
                    case SpeedRegulatorMode.Auto:
                        {
                            if (SpeedRegulatorOptions.Contains("regulatorauto")) test = true;
                            if (!DisableManualSwitchToAutoWhenSetSpeedNotAtTop && !KeepSelectedSpeedWhenManualModeSet) SelectedSpeedMpS = Locomotive.AbsSpeedMpS;
                            break;
                        }
                    case SpeedRegulatorMode.Testing: if (SpeedRegulatorOptions.Contains("regulatortest")) test = true; break;
                }
                if (!test && SpeedRegMode == SpeedRegulatorMode.Testing) // if we're here, then it means no higher option, return to previous state and get out
                {
                    SpeedRegMode = previousMode;
                    return;
                }
            }
            Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Speed regulator mode changed to {0}", Simulator.Catalog.GetString(SpeedRegMode.ToString())));
        }
        public void SpeedRegulatorModeDecrease()
        {
            Locomotive.SignalEvent(Common.Event.CruiseControlSpeedRegulator);
            if (SpeedRegMode == SpeedRegulatorMode.Manual) return;
            if (SpeedRegMode == SpeedRegulatorMode.Auto &&
                (DisableManualSwitchToManualWhenSetForceNotAtZero && SelectedMaxAccelerationStep != 0))
                return;
            bool test = false;
            while (!test)
            {
                SpeedRegMode--;
                switch (SpeedRegMode)
                {
                    case SpeedRegulatorMode.Auto: if (SpeedRegulatorOptions.Contains("regulatorauto")) test = true; break;
                    case SpeedRegulatorMode.Manual:
                        {
                            Locomotive.ThrottleController.SetPercent(0);
                            if (SpeedRegulatorOptions.Contains("regulatormanual")) test = true;
                            if (ZeroSelectedSpeedWhenPassingToThrottleMode || UseThrottleAsSpeedSelector) SelectedSpeedMpS = 0;
                            if (UseThrottleAsForceSelector) SelectedMaxAccelerationStep = 0;
                            break;
                        }
                }
                if (!test && SpeedRegMode == SpeedRegulatorMode.Manual)
                    return;
            }
            Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Speed regulator mode changed to {0}", Simulator.Catalog.GetString(SpeedRegMode.ToString())));
        }
        public void SpeedSelectorModeStartIncrease()
        {
            Locomotive.SignalEvent(Common.Event.CruiseControlSpeedSelector);
            if (SpeedSelMode == SpeedSelectorMode.Start) return;
            bool test = false;
            while (!test)
            {
                SpeedSelMode++;
                if (SpeedSelMode != SpeedSelectorMode.Parking && !EngineBrakePriority) Locomotive.SetEngineBrakePercent(0);
                switch (SpeedSelMode)
                {
                    case SpeedSelectorMode.Neutral: if (SpeedRegulatorOptions.Contains("selectorneutral")) test = true; break;
                    case SpeedSelectorMode.On: if (SpeedRegulatorOptions.Contains("selectoron")) test = true; break;
                    case SpeedSelectorMode.Start: if (SpeedRegulatorOptions.Contains("selectorstart")) test = true; break;
                }
                if (!test && SpeedSelMode == SpeedSelectorMode.Start)
                    return;
            }
            Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetString("Speed selector mode changed to") + " " + Simulator.Catalog.GetString(SpeedSelMode.ToString()));
        }
        public void SpeedSelectorModeStopIncrease()
        {
            Locomotive.SignalEvent(Common.Event.CruiseControlSpeedSelector);
            if (SpeedSelMode == SpeedSelectorMode.Start)
            {
                bool test = false;
                while (!test)
                {
                    SpeedSelMode--;
                    switch (SpeedSelMode)
                    {
                        case SpeedSelectorMode.On: if (SpeedRegulatorOptions.Contains("selectoron")) test = true; break;
                        case SpeedSelectorMode.Neutral: if (SpeedRegulatorOptions.Contains("selectorneutral")) test = true; break;
                        case SpeedSelectorMode.Parking: if (SpeedRegulatorOptions.Contains("selectorparking")) test = true; break;
                    }
                    if (!test && SpeedSelMode == SpeedSelectorMode.Parking && !EngineBrakePriority)
                        return;
                }
            }
            Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Speed selector mode changed to {0}", Simulator.Catalog.GetString(SpeedSelMode.ToString())));
        }
        public void SpeedSelectorModeDecrease()
        {
            Locomotive.SignalEvent(Common.Event.CruiseControlSpeedSelector);
            SpeedSelectorMode previousMode = SpeedSelMode;
            if (SpeedSelMode == SpeedSelectorMode.Parking && !EngineBrakePriority) return;
            bool test = false;
            while (!test)
            {
                SpeedSelMode--;
                switch (SpeedSelMode)
                {
                    case SpeedSelectorMode.On: if (SpeedRegulatorOptions.Contains("selectoron")) test = true; break;
                    case SpeedSelectorMode.Neutral: if (SpeedRegulatorOptions.Contains("selectorneutral")) test = true; break;
                    case SpeedSelectorMode.Parking: if (SpeedRegulatorOptions.Contains("selectorparking")) test = true; break;
                }
                if (!test && SpeedSelMode == SpeedSelectorMode.Parking && !EngineBrakePriority)
                {
                    SpeedSelMode = previousMode;
                    return;
                }
            }
            Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Speed selector mode changed to {0}", Simulator.Catalog.GetString(SpeedSelMode.ToString())));
        }

        public void SetMaxForcePercent(float percent)
        {
            if (SelectedMaxAccelerationPercent == percent) return;
            SelectedMaxAccelerationPercent = percent;
            SelectedMaxAccelerationStep = (float)Math.Round(SelectedMaxAccelerationPercent * SpeedRegulatorMaxForceSteps / 100, 0);
            Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Speed regulator max acceleration percent changed to {0}", Simulator.Catalog.GetString(SelectedMaxAccelerationPercent.ToString()) + "%"));
        }

        bool maxForceIncreasing = false;
        public void SpeedRegulatorMaxForceStartIncrease()
        {
            if (SelectedMaxAccelerationStep == 0)
            {
                Locomotive.SignalEvent(Common.Event.LeverFromZero);
            }
            Locomotive.SignalEvent(Common.Event.CruiseControlMaxForce);
            if (SelectedMaxAccelerationStep == 0 && DisableCruiseControlOnThrottleAndZeroForce && ForceRegulatorAutoWhenNonZeroForceSelected &&
                Locomotive.ThrottleController.CurrentValue == 0 && Locomotive.DynamicBrakeController.CurrentValue == 0 && Locomotive.CruiseControl.SpeedRegMode == SpeedRegulatorMode.Manual)
            {
                SpeedRegMode = SpeedRegulatorMode.Auto;
                WasForceReset = true;
            }
            maxForceIncreasing = true;
            speedRegulatorIntermediateValue = SpeedRegulatorMaxForcePercentUnits ? SelectedMaxAccelerationPercent : SelectedMaxAccelerationStep;
        }
        public void SpeedRegulatorMaxForceStopIncrease()
        {
            maxForceIncreasing = false;
        }
        protected void SpeedRegulatorMaxForceIncrease(float elapsedClockSeconds)
        {
            Locomotive.SignalEvent(Common.Event.CruiseControlMaxForce);
            if (MaxForceSetSingleStep) maxForceIncreasing = false;
            if (SelectedMaxAccelerationStep == 0.5f) SelectedMaxAccelerationStep = 0;
            if (SpeedRegulatorMaxForcePercentUnits)
            {
                if (SelectedMaxAccelerationPercent == 100)
                    return;
                speedRegulatorIntermediateValue += StepSize * elapsedClockSeconds;
                SelectedMaxAccelerationPercent = (float)Math.Truncate(speedRegulatorIntermediateValue + 1);
                //                SelectedMaxAccelerationPercent = (float)Math.Round(SelectedMaxAccelerationStep * 100 / SpeedRegulatorMaxForceSteps, 0);
                Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Speed regulator max acceleration percent changed to {0}", Simulator.Catalog.GetString(SelectedMaxAccelerationPercent.ToString()) + "%"));
            }
            else
            {
                if (SelectedMaxAccelerationStep == SpeedRegulatorMaxForceSteps)
                    return;
                speedRegulatorIntermediateValue += MaxForceSelectorIsDiscrete ? elapsedClockSeconds : StepSize * elapsedClockSeconds * SpeedRegulatorMaxForceSteps / 100.0f;
                SelectedMaxAccelerationStep = (float)Math.Truncate(speedRegulatorIntermediateValue + 1);
                Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetString("Speed regulator max acceleration changed to") + " " + Simulator.Catalog.GetString(Math.Round(SelectedMaxAccelerationStep * 100 / SpeedRegulatorMaxForceSteps, 0).ToString()));
            }
        }

        protected bool maxForceDecreasing = false;
        public void SpeedRegulatorMaxForceStartDecrease()
        {
            Locomotive.SignalEvent(Common.Event.CruiseControlMaxForce);
            maxForceDecreasing = true;
            speedRegulatorIntermediateValue = SpeedRegulatorMaxForcePercentUnits ? SelectedMaxAccelerationPercent : SelectedMaxAccelerationStep;
        }
        public void SpeedRegulatorMaxForceStopDecrease()
        {
            maxForceDecreasing = false;
        }
        protected void SpeedRegulatorMaxForceDecrease(float elapsedClockSeconds)
        {
            Locomotive.SignalEvent(Common.Event.CruiseControlMaxForce);
            if (MaxForceSetSingleStep) maxForceDecreasing = false;
            if (DisableZeroForceStep)
            {
                if (SelectedMaxAccelerationStep <= 1)
                    return;
            }
            else
            {
                if (SelectedMaxAccelerationStep <= 0)
                    return;
            }
            speedRegulatorIntermediateValue -= MaxForceSelectorIsDiscrete ? elapsedClockSeconds : StepSize * elapsedClockSeconds * SpeedRegulatorMaxForceSteps / 100.0f;
            SelectedMaxAccelerationStep = (float)Math.Truncate(speedRegulatorIntermediateValue);
            if (DisableZeroForceStep)
            {
                if (SelectedMaxAccelerationStep <= 1)
                {
                    Locomotive.SignalEvent(Common.Event.LeverToZero);
                }
            }
            else
            {
                if (SelectedMaxAccelerationStep <= 0)
                {
                    Locomotive.SignalEvent(Common.Event.LeverToZero);
                }
            }
            Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Speed regulator max acceleration changed to {0}", Simulator.Catalog.GetString(Math.Round(SelectedMaxAccelerationStep * 100 / SpeedRegulatorMaxForceSteps, 0).ToString())));
        }

        public void SpeedRegulatorMaxForceChangeByMouse(float movExtension, float maxValue)
        {
            if (movExtension != 0 && SelectedMaxAccelerationStep == 0 && DisableCruiseControlOnThrottleAndZeroForce && ForceRegulatorAutoWhenNonZeroForceSelected &&
                Locomotive.ThrottleController.CurrentValue == 0 && Locomotive.DynamicBrakeController.CurrentValue == 0 && SpeedRegMode == SpeedRegulatorMode.Manual)
            {
                SpeedRegMode = SpeedRegulatorMode.Auto;
                WasForceReset = true;
            }
            if (SelectedMaxAccelerationStep == 0)
            {
                if (movExtension > 0)
                {
                    Locomotive.SignalEvent(Common.Event.LeverFromZero);
                }
                else if (movExtension < 0)
                    return;
            }
            if (movExtension == 1)
            {
                SelectedMaxAccelerationStep += 1;
            }
            if (movExtension == -1)
            {
                SelectedMaxAccelerationStep -= 1;
            }
            if (movExtension != 0)
            {
                SelectedMaxAccelerationStep += movExtension * maxValue;
                if (SelectedMaxAccelerationStep > SpeedRegulatorMaxForceSteps)
                    SelectedMaxAccelerationStep = SpeedRegulatorMaxForceSteps;
                if (SelectedMaxAccelerationStep < 0)
                    SelectedMaxAccelerationStep = 0;
                if (SelectedMaxAccelerationStep == 0)
                {
                    Locomotive.SignalEvent(Common.Event.LeverToZero);
                }
                Locomotive.Simulator.Confirmer.Information(Simulator.Catalog.GetStringFmt("Selected maximum acceleration was changed to {0}", Math.Round((MaxForceSelectorIsDiscrete ?
                    (int)SelectedMaxAccelerationStep : SelectedMaxAccelerationStep) * 100 / SpeedRegulatorMaxForceSteps, 0).ToString() + "%"));
            }
        }

        public bool selectedSpeedIncreasing = false;
        public void SpeedRegulatorSelectedSpeedStartIncrease()
        {
            var mpc = Locomotive.MultiPositionControllers.Where(x => 
                x.controllerBinding == ControllerBinding.SelectedSpeed && !x.StateChanged).FirstOrDefault();
            if (mpc != null)
            {
                mpc.StateChanged = true;
                if (SpeedRegMode != SpeedRegulatorMode.Auto && (ForceRegulatorAutoWhenNonZeroSpeedSelected ||
                    SelectedMaxAccelerationStep == 0 && DisableCruiseControlOnThrottleAndZeroForce && ForceRegulatorAutoWhenNonZeroSpeedSelectedAndThrottleAtZero &&
                    Locomotive.ThrottleController.CurrentValue == 0 && Locomotive.DynamicBrakeController.CurrentValue == 0))
                {
                    SpeedRegMode = SpeedRegulatorMode.Auto;
                }

                mpc.DoMovement(MultiPositionController.Movement.Forward);
                return;
            }
            if (SpeedRegMode != SpeedRegulatorMode.Auto && (ForceRegulatorAutoWhenNonZeroSpeedSelected || HasProportionalSpeedSelector &&
                SelectedMaxAccelerationStep == 0 && DisableCruiseControlOnThrottleAndZeroForce && ForceRegulatorAutoWhenNonZeroSpeedSelectedAndThrottleAtZero &&
                            Locomotive.ThrottleController.CurrentValue == 0 && Locomotive.DynamicBrakeController.CurrentValue == 0))
            {
                SpeedRegMode = SpeedRegulatorMode.Auto;
            }
            if (SpeedRegMode != SpeedRegulatorMode.Auto && (ForceRegulatorAutoWhenNonZeroSpeedSelectedAndThrottleAtZero &&
                SelectedMaxAccelerationStep == 0 && DisableCruiseControlOnThrottleAndZeroForceAndZeroSpeed &&
                            Locomotive.ThrottleController.CurrentValue == 0 && Locomotive.DynamicBrakeController.CurrentValue == 0))
            {
                SpeedRegMode = SpeedRegulatorMode.Auto;
            }
            if (UseThrottleAsSpeedSelector || HasProportionalSpeedSelector)
            {
                selectedSpeedIncreasing = true;
                if (SelectedSpeedMpS == 0)
                {
                    Locomotive.SignalEvent(Common.Event.LeverFromZero);
                }
            }
            else
                SpeedSelectorModeStartIncrease();
        }
        public void SpeedRegulatorSelectedSpeedStopIncrease()
        {
            var mpc = Locomotive.MultiPositionControllers.Where(x => x.controllerBinding == ControllerBinding.SelectedSpeed).FirstOrDefault();
            if (mpc != null)
            {
                mpc.StateChanged = false;
                mpc.DoMovement(Controllers.MultiPositionController.Movement.Neutral);
                return;
            }
            if (UseThrottleAsSpeedSelector || HasProportionalSpeedSelector)
                selectedSpeedIncreasing = false;
            else
                SpeedSelectorModeStopIncrease();
        }

        protected double selectedSpeedLeverHoldTime = 0;
        public void SpeedRegulatorSelectedSpeedIncrease()
        {
            if (selectedSpeedLeverHoldTime + SpeedSelectorStepTimeSeconds > TotalTime)
                return;
            selectedSpeedLeverHoldTime = TotalTime;

            SelectedSpeedMpS = Math.Max(MinimumSpeedForCCEffectMpS, SelectedSpeedMpS + SpeedRegulatorNominalSpeedStepMpS);
            if (SelectedSpeedMpS > Locomotive.MaxSpeedMpS)
                SelectedSpeedMpS = Locomotive.MaxSpeedMpS;
            if (SpeedIsMph)
                Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Selected speed changed to {0} mph", Math.Round(MpS.FromMpS(SelectedSpeedMpS, false), 0, MidpointRounding.AwayFromZero).ToString()));
            else
                Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Selected speed changed to {0} km/h", Math.Round(MpS.FromMpS(SelectedSpeedMpS, true), 0, MidpointRounding.AwayFromZero).ToString()));
        }

        public bool SelectedSpeedDecreasing = false;
        public void SpeedRegulatorSelectedSpeedStartDecrease()
        {
            var mpc = Locomotive.MultiPositionControllers.Where(x => x.controllerBinding == ControllerBinding.SelectedSpeed && !x.StateChanged).FirstOrDefault();
            if (mpc != null)
            {
                mpc.StateChanged = true;
                mpc.DoMovement(Controllers.MultiPositionController.Movement.Aft);
                return;
            }
            if (UseThrottleAsSpeedSelector || HasProportionalSpeedSelector)
                SelectedSpeedDecreasing = true;
            else
                SpeedSelectorModeDecrease();
        }
        public void SpeedRegulatorSelectedSpeedStopDecrease()
        {
            var mpc = Locomotive.MultiPositionControllers.Where(x => x.controllerBinding == ControllerBinding.SelectedSpeed).FirstOrDefault();
            if (mpc != null)
            {
                mpc.StateChanged = false;
                mpc.DoMovement(Controllers.MultiPositionController.Movement.Neutral);
                return;
            }
            SelectedSpeedDecreasing = false;
        }
        public void SpeedRegulatorSelectedSpeedDecrease()
        {
            if (selectedSpeedLeverHoldTime + SpeedSelectorStepTimeSeconds > TotalTime)
                return;
            selectedSpeedLeverHoldTime = TotalTime;
            if (SelectedSpeedMpS == 0)
                return;
            SelectedSpeedMpS -= SpeedRegulatorNominalSpeedStepMpS;
            if (SelectedSpeedMpS < 0)
                SelectedSpeedMpS = 0f;
            if (MinimumSpeedForCCEffectMpS > 0 && SelectedSpeedMpS < MinimumSpeedForCCEffectMpS)
                SelectedSpeedMpS = 0;
            if (SpeedRegMode == SpeedRegulatorMode.Auto && ForceRegulatorAutoWhenNonZeroSpeedSelected && SelectedSpeedMpS == 0)
            {
                // return back to manual, clear all we have controlled before and let the driver to set up new stuff
                SpeedRegMode = SpeedRegulatorMode.Manual;
                //                Locomotive.ThrottleController.SetPercent(0);
                //                Locomotive.SetDynamicBrakePercent(0);
                Locomotive.DynamicBrakeChangeActiveState(false);
            }
            if (SelectedSpeedMpS == 0)
            {
                if (HasProportionalSpeedSelector)
                {
                    Locomotive.SignalEvent(Common.Event.LeverToZero);
                }
                SelectedSpeedDecreasing = false;
            }
            if (SpeedIsMph)
                Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Selected speed changed to {0} mph", Math.Round(MpS.FromMpS(SelectedSpeedMpS, false), 0, MidpointRounding.AwayFromZero).ToString()));
            else
                Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Selected speed changed to {0} km/h", Math.Round(MpS.FromMpS(SelectedSpeedMpS, true), 0, MidpointRounding.AwayFromZero).ToString()));
        }

        public void SpeedRegulatorSelectedSpeedChangeByMouse(float movExtension, bool metric, float maxValue)
        {
            if (movExtension != 0 && SelectedMaxAccelerationStep == 0 && DisableCruiseControlOnThrottleAndZeroForce && ForceRegulatorAutoWhenNonZeroSpeedSelectedAndThrottleAtZero &&
            Locomotive.ThrottleController.CurrentValue == 0 && Locomotive.DynamicBrakeController.CurrentValue == 0 && SpeedRegMode == SpeedRegulatorMode.Manual)
                SpeedRegMode = SpeedRegulatorMode.Auto;
            if (movExtension != 0)
            {
                if (SelectedSpeedMpS == 0)
                {
                    if (movExtension > 0)
                    {
                        Locomotive.SignalEvent(Common.Event.LeverFromZero);
                    }
                    else if (movExtension < 0)
                        return;
                }
                var deltaSpeed = SpeedSelectorIsDiscrete ? (metric ? MpS.FromKpH((float)Math.Round(movExtension * maxValue / SpeedRegulatorNominalSpeedStepKpHOrMpH) * SpeedRegulatorNominalSpeedStepKpHOrMpH) :
                    MpS.FromMpH((float)Math.Round(movExtension * maxValue / SpeedRegulatorNominalSpeedStepKpHOrMpH) * SpeedRegulatorNominalSpeedStepKpHOrMpH)) :
                    (metric ? MpS.FromKpH((float)Math.Round(movExtension * maxValue)) :
                    MpS.FromMpH((float)Math.Round(movExtension * maxValue)));
                if (deltaSpeed > 0)
                    SelectedSpeedMpS = Math.Max(SelectedSpeedMpS + deltaSpeed, MinimumSpeedForCCEffectMpS);
                else
                {
                    SelectedSpeedMpS += deltaSpeed;
                    if (MinimumSpeedForCCEffectMpS > 0 && SelectedSpeedMpS < MinimumSpeedForCCEffectMpS)
                        SelectedSpeedMpS = 0;
                }

                if (SelectedSpeedMpS > Locomotive.MaxSpeedMpS)
                    SelectedSpeedMpS = Locomotive.MaxSpeedMpS;
                if (SelectedSpeedMpS < 0)
                    SelectedSpeedMpS = 0;
                if (SelectedSpeedMpS == 0 && movExtension < 0)
                {
                    Locomotive.SignalEvent(Common.Event.LeverToZero);
                }
                if (SpeedIsMph)
                    Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Selected speed changed to {0}", Math.Round(MpS.FromMpS(SelectedSpeedMpS, false), 0, MidpointRounding.AwayFromZero).ToString() + " mph"));
                else
                    Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetStringFmt("Selected speed changed to {0}", Math.Round(MpS.FromMpS(SelectedSpeedMpS, true), 0, MidpointRounding.AwayFromZero).ToString() + " km/h"));
            }
        }

        public void NumerOfAxlesIncrease()
        {
            NumerOfAxlesIncrease(1);
        }
        public void NumerOfAxlesIncrease(int ByAmount)
        {
            SelectedNumberOfAxles += ByAmount;
            trainLength = SelectedNumberOfAxles * 6.6f;
            TrainLengthMeters = (int)Math.Round(trainLength + 0.5, 0);
            Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetString("Number of axles increased to ") + SelectedNumberOfAxles.ToString());
        }
        public void NumberOfAxlesDecrease()
        {
            NumberOfAxlesDecrease(1);
        }
        public void NumberOfAxlesDecrease(int ByAmount)
        {
            if ((SelectedNumberOfAxles - ByAmount) < 1) return;
            SelectedNumberOfAxles -= ByAmount;
            trainLength = SelectedNumberOfAxles * 6.6f;
            TrainLengthMeters = (int)Math.Round(trainLength + 0.5, 0);
            Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetString("Number of axles decreased to ") + SelectedNumberOfAxles.ToString());
        }
        public void ActivateRestrictedSpeedZone()
        {
            RestrictedRegionOdometer.Setup(TrainLengthMeters);
            if (!RestrictedRegionOdometer.Started)
            {
                RestrictedRegionOdometer.Start();
                CurrentSelectedSpeedMpS = SelectedSpeedMpS;
            }
            Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetString("Speed restricted zone active."));
        }

        public void SetSpeed(float Speed)
        {
            if (SpeedRegMode == SpeedRegulatorMode.Manual && (ForceRegulatorAutoWhenNonZeroSpeedSelected || ForceRegulatorAutoWhenNonZeroSpeedSelectedAndThrottleAtZero))
                SpeedRegMode = SpeedRegulatorMode.Auto;
            if (SpeedRegMode == SpeedRegulatorMode.Manual)
                return;
            Locomotive.SignalEvent(Common.Event.CruiseControlAlert1);
            var requiredSpeedMpS = SpeedIsMph ? MpS.FromMpH(Speed) : MpS.FromKpH(Speed);
            if (MinimumSpeedForCCEffectMpS == 0)
                SelectedSpeedMpS = requiredSpeedMpS;
            else if (requiredSpeedMpS > SelectedSpeedMpS)
                SelectedSpeedMpS = Math.Max(requiredSpeedMpS, MinimumSpeedForCCEffectMpS);
            else if (requiredSpeedMpS < MinimumSpeedForCCEffectMpS)
                SelectedSpeedMpS = 0;
            else
                SelectedSpeedMpS = requiredSpeedMpS;
            if (SelectedSpeedMpS > Locomotive.MaxSpeedMpS)
                SelectedSpeedMpS = Locomotive.MaxSpeedMpS;
            Simulator.Confirmer.Message(ConfirmLevel.Information, Simulator.Catalog.GetString("Selected speed set to ") + Speed.ToString() + (SpeedIsMph ? "mph" : "kmh"));
        }

        public void CalculateRequiredForce(float elapsedClockSeconds, float AbsWheelSpeedMpS)
        {
            float speedDiff = Locomotive.Train.Cars.Where(x => x is MSTSLocomotive).Select(x => (x as MSTSLocomotive).AbsWheelSpeedMpS - x.AbsSpeedMpS).Max();

            float trainElevation = Locomotive.Train.Cars.Select(tc => tc.Flipped ? tc.CurrentElevationPercent : -tc.CurrentElevationPercent).Sum() / Locomotive.Train.Cars.Count;

            if (Locomotive.TrainBrakeController.TrainBrakeControllerState == ControllerState.Release ||
                Locomotive.TrainBrakeController.TrainBrakeControllerState == ControllerState.Neutral)
            {
                TrainBrakePriority = false;
            }

            if ((SpeedSelMode == SpeedSelectorMode.On || SpeedSelMode == SpeedSelectorMode.Start) && !TrainBrakePriority)
            {
                if (Locomotive.AbsSpeedMpS == 0)
                {
                    timeFromEngineMoved = 0;
                    reducingForce = true;
                }
                else if (reducingForce)
                {

                    timeFromEngineMoved += elapsedClockSeconds;
                    float timeToReduce = Locomotive.SelectedTrainType == MSTSLocomotive.TrainType.Pax ? PowerReductionDelayPaxTrain : PowerReductionDelayCargoTrain;
                    if (timeFromEngineMoved > timeToReduce)
                        reducingForce = false;
                }
            }
            else
            {
                timeFromEngineMoved = 0;
                reducingForce = true;
            }

            if (TrainBrakePriority)
            {
                WasForceReset = false;
                WasBraking = true;
            }
            else if (SpeedSelMode == SpeedSelectorMode.Start) WasForceReset = true;
            else if (SelectedMaxAccelerationPercent == 0 && SelectedMaxAccelerationStep == 0)
            {
                WasBraking = false;
                WasForceReset = true;
            }
            if (TrainBrakePriority || (ThrottleNeutralPosition && SelectedSpeedMpS == 0) || SelectedMaxAccelerationStep == 0 ||
                (ForceResetRequiredAfterBraking &&
                    (!WasForceReset || (WasBraking && (SelectedMaxAccelerationStep > 0 || SelectedMaxAccelerationPercent > 0)))))
            {
                if (SpeedSelMode == SpeedSelectorMode.Parking)
                    if (AbsWheelSpeedMpS < (SpeedIsMph ? MpS.FromMpH(ParkingBrakeEngageSpeed) : MpS.FromKpH(ParkingBrakeEngageSpeed)))
                        Locomotive.SetEngineBrakePercent(ParkingBrakePercent);
                CCThrottleOrDynBrakePercent = 0;
                trainBrakePercent = 0;
                return;
            }
            if (Locomotive.TrainBrakeController.MaxPressurePSI - Locomotive.BrakeSystem.BrakeLine1PressurePSI > 1)
            {
                if (!UseTrainBrakeAndDynBrake || !CCIsUsingTrainBrake)
                {
                    reducingForce = true;
                    timeFromEngineMoved = 0;
                    if (CCThrottleOrDynBrakePercent > 0)
                        CCThrottleOrDynBrakePercent = 0;
                    return;
                }
            }
            else if (trainBrakePercent == 0) CCIsUsingTrainBrake = false;

            if (SpeedRegulatorOptions.Contains("engageforceonnonzerospeed") && SelectedSpeedMpS > 0)
            {
                SpeedSelMode = SpeedSelectorMode.On;
                SpeedRegMode = SpeedRegulatorMode.Auto;
                SkipThrottleDisplay = true;
                reducingForce = false;
            }

            if (firstIteration) // if this is exetuted the first time, let's check all other than player engines in the consist, and record them for further throttle manipulation
            {
                if (!DoComputeNumberOfAxles) SelectedNumberOfAxles = (int)(Locomotive.Train.Length / 6.6f); // also set the axles, for better delta computing, if user omits to set it
                firstIteration = false;
            }

            float deltaSpeedMpS = SetSpeedMpS - AbsWheelSpeedMpS;
            if (SpeedSelMode == SpeedSelectorMode.Parking && !EngineBrakePriority)
            {
                if (CCThrottleOrDynBrakePercent > 0 || AbsWheelSpeedMpS == 0)
                {
                    CCThrottleOrDynBrakePercent = 0;
                }

                if (AbsWheelSpeedMpS < (SpeedIsMph ? MpS.FromMpH(ParkingBrakeEngageSpeed) : MpS.FromKpH(ParkingBrakeEngageSpeed)))
                    Locomotive.SetEngineBrakePercent(ParkingBrakePercent);
            }
            else if (SpeedSelMode == SpeedSelectorMode.Neutral || SpeedSelMode < SpeedSelectorMode.Start && !SpeedRegulatorOptions.Contains("startfromzero") && AbsWheelSpeedMpS < SafeSpeedForAutomaticOperationMpS)
            {
                if (deltaSpeedMpS >= 0)
                {
                    // Progressively stop accelerating/braking: reach 0
                    if (CCThrottleOrDynBrakePercent < 0) IncreaseForce(ref CCThrottleOrDynBrakePercent, elapsedClockSeconds, 0);
                    else if (CCThrottleOrDynBrakePercent > 0) DecreaseForce(ref CCThrottleOrDynBrakePercent, elapsedClockSeconds, 0);
                    trainBrakePercent = 0;
                }
                else // start braking
                {
                    if (CCThrottleOrDynBrakePercent > 0)
                    {
                        DecreaseForce(ref CCThrottleOrDynBrakePercent, elapsedClockSeconds, 0);
                    }
                    else
                    {
                        deltaSpeedMpS = SetSpeedMpS + (trainElevation < -0.01 ? trainElevation * (SelectedNumberOfAxles / 12) : 0) - AbsWheelSpeedMpS;
                        if (Locomotive.DynamicBrakeAvailable)
                        {
                            AccelerationDemandMpSS = (float)-Math.Sqrt(-StartReducingSpeedDeltaDownwards * deltaSpeedMpS);

                            if (CCThrottleOrDynBrakePercent < -(AccelerationDemandMpSS * 100) && AccelerationDemandMpSS < -0.05f)
                            {
                                float maxPercent = DynamicBrakeIsSelectedForceDependant ? ((MaxForceSelectorIsDiscrete ? (int)SelectedMaxAccelerationStep : SelectedMaxAccelerationStep) * 100 / SpeedRegulatorMaxForceSteps) : 100;
                                DecreaseForce(ref CCThrottleOrDynBrakePercent, elapsedClockSeconds, -maxPercent);
                            }
                            if (CCThrottleOrDynBrakePercent > -((AccelerationDemandMpSS - 0.05f) * 100))
                            {
                                IncreaseForce(ref CCThrottleOrDynBrakePercent, elapsedClockSeconds, 0);
                            }
                            if (DynamicBrakeIsSelectedForceDependant)
                            {
                                float maxPercent = (MaxForceSelectorIsDiscrete ? (int)SelectedMaxAccelerationStep : SelectedMaxAccelerationStep) * 100 / SpeedRegulatorMaxForceSteps;
                                if (CCThrottleOrDynBrakePercent < -maxPercent)
                                    CCThrottleOrDynBrakePercent = -maxPercent;
                            }
                        }
                        if (UseTrainBrakeAndDynBrake || !Locomotive.DynamicBrakeAvailable) // use TrainBrake
                            SetTrainBrake(ref trainBrakePercent, elapsedClockSeconds, deltaSpeedMpS);
                    }
                }
            }

            if ((AbsWheelSpeedMpS > SafeSpeedForAutomaticOperationMpS || SpeedSelMode == SpeedSelectorMode.Start || SpeedRegulatorOptions.Contains("startfromzero")) && (SpeedSelMode != SpeedSelectorMode.Neutral && SpeedSelMode != SpeedSelectorMode.Parking))
            {
                float coeff = Math.Max(MpS.FromMpS(Locomotive.WheelSpeedMpS, !SpeedIsMph) / 100 * 1.2f, 1);
                if (deltaSpeedMpS >= 0)
                {
                    AccelerationDemandMpSS = (float)Math.Sqrt(StartReducingSpeedDelta * coeff * deltaSpeedMpS);
                    if ((SpeedSelMode == SpeedSelectorMode.On || SpeedSelMode == SpeedSelectorMode.Start) && CCThrottleOrDynBrakePercent < 0)
                    {
                        IncreaseForce(ref CCThrottleOrDynBrakePercent, elapsedClockSeconds, 0);
                    }
                    trainBrakePercent = 0;
                }
                else // start braking
                {
                    if (CCThrottleOrDynBrakePercent > 0)
                    {
                        DecreaseForce(ref CCThrottleOrDynBrakePercent, elapsedClockSeconds, 0);
                    }
                    else
                    {
                        if (Locomotive.DynamicBrakeAvailable)
                        {
                            float val = (float)Math.Abs(StartReducingSpeedDeltaDownwards * coeff * ((deltaSpeedMpS + 0.5f) / 3));
                            AccelerationDemandMpSS = -(float)Math.Sqrt(val);
                            if (RelativeAccelerationMpSS > AccelerationDemandMpSS)
                            {
                                float maxStep = (RelativeAccelerationMpSS - AccelerationDemandMpSS) * 2; 
                                DecreaseForce(ref CCThrottleOrDynBrakePercent, elapsedClockSeconds, Math.Max(CCThrottleOrDynBrakePercent-maxStep, -100));
                            }
                            else if (RelativeAccelerationMpSS + 0.01f < AccelerationDemandMpSS)
                            {
                                float maxStep = (AccelerationDemandMpSS - RelativeAccelerationMpSS) * 2;
                                IncreaseForce(ref CCThrottleOrDynBrakePercent, elapsedClockSeconds, Math.Min(CCThrottleOrDynBrakePercent+maxStep, 0));
                            }
                            if (DynamicBrakeIsSelectedForceDependant)
                            {
                                float maxPercent = (MaxForceSelectorIsDiscrete ? (int)SelectedMaxAccelerationStep : SelectedMaxAccelerationStep) * 100 / SpeedRegulatorMaxForceSteps;
                                if (CCThrottleOrDynBrakePercent < -maxPercent)
                                    CCThrottleOrDynBrakePercent = -maxPercent;
                            }
                        }
                        if (UseTrainBrakeAndDynBrake || !Locomotive.DynamicBrakeAvailable) // use TrainBrake
                            SetTrainBrake(ref trainBrakePercent, elapsedClockSeconds, deltaSpeedMpS);
                    }
                }

                if (Locomotive.Direction != Direction.N)
                {
                    float a = 0;
                    float demandedPercent = 0;
                    if (CCThrottleOrDynBrakePercent >= 0 && RelativeAccelerationMpSS < AccelerationDemandMpSS)
                    {
                        float newThrottle = 0;
                        // calculate new max force if MaxPowerThreshold is set
                        if (MaxPowerThreshold > 0)
                        {
                            float currentSpeed = MpS.FromMpS(AbsWheelSpeedMpS, !SpeedIsMph);
                            float percentComplete = (int)Math.Round((double)(100 * currentSpeed) / MaxPowerThreshold);
                            if (percentComplete > 100)
                                percentComplete = 100;
                            newThrottle = percentComplete;
                        }
                        if (ForceStepsThrottleTable.Count > 0)
                        {
                            demandedPercent = ForceStepsThrottleTable[(int)SelectedMaxAccelerationStep - 1];
                            if (AccelerationTable.Count > 0)
                                a = AccelerationTable[(int)SelectedMaxAccelerationStep - 1];
                        }
                        else
                            demandedPercent = (MaxForceSelectorIsDiscrete ? (int)SelectedMaxAccelerationStep : SelectedMaxAccelerationStep) * 100 / SpeedRegulatorMaxForceSteps;
                        if (demandedPercent < newThrottle)
                            demandedPercent = newThrottle;
                    }
                    if (reducingForce)
                    {
                        if (demandedPercent > PowerReductionValue)
                            demandedPercent = PowerReductionValue;
                    }
                    if (deltaSpeedMpS > 0 && CCThrottleOrDynBrakePercent >= 0)
                    {
                        float? target = null;
                        if (a > 0 && MpS.FromMpS(Locomotive.WheelSpeedMpS, !SpeedIsMph) > 5)
                        {
                            if (Locomotive.AccelerationMpSS < a - 0.02 && deltaSpeedMpS > 0.8f) target = 100;
                            else if ((Locomotive.AccelerationMpSS < a - 0.02 && CCThrottleOrDynBrakePercent < demandedPercent) ||
                                    (CCThrottleOrDynBrakePercent > demandedPercent && (deltaSpeedMpS < 0.8f || Locomotive.AccelerationMpSS > a + 0.02)))
                            {
                                target = demandedPercent;
                            }
                        }
                        else
                        {
                            if (CCThrottleOrDynBrakePercent < demandedPercent)
                            {
                                float accelDiff = AccelerationDemandMpSS - Locomotive.AccelerationMpSS;
                                target = Math.Min(CCThrottleOrDynBrakePercent + accelDiff * 10, demandedPercent);
                            }
                            else
                                target = demandedPercent;
                        }
                        if (target > CCThrottleOrDynBrakePercent)
                            IncreaseForce(ref CCThrottleOrDynBrakePercent, elapsedClockSeconds, target.Value);
                        else if (target < CCThrottleOrDynBrakePercent)
                            DecreaseForce(ref CCThrottleOrDynBrakePercent, elapsedClockSeconds, target.Value);
                    }
                }
            }

            if (Locomotive.WheelSpeedMpS == 0 && CCThrottleOrDynBrakePercent < 0)
                CCThrottleOrDynBrakePercent = 0;

            float current = Math.Abs(Locomotive.TractiveForceN) / Locomotive.MaxForceN * Locomotive.MaxCurrentA;
            if (current < PowerBreakoutAmpers)
                breakout = true;
            if (breakout && deltaSpeedMpS > 0.2f)
                breakout = false;
            if (CCThrottleOrDynBrakePercent > 0)
            {
                if (speedDiff > AntiWheelSpinSpeedDiffThreshold)
                {
                    skidSpeedDegratation += 0.05f;
                }
                else if (skidSpeedDegratation > 0)
                {
                    skidSpeedDegratation -= 0.1f;
                }
                if (speedDiff < AntiWheelSpinSpeedDiffThreshold - 0.05f)
                    skidSpeedDegratation = 0;
                if (AntiWheelSpinEquipped) CCThrottleOrDynBrakePercent = Math.Max(CCThrottleOrDynBrakePercent - skidSpeedDegratation, 0);
                if (breakout || Locomotive.TrainBrakeController.MaxPressurePSI - Locomotive.BrakeSystem.BrakeLine1PressurePSI > 1)
                {
                    CCThrottleOrDynBrakePercent = 0;
                }
            }
        }

        void IncreaseForce(ref float throttleOrDynPercent, float elapsedClockSeconds, float maxPercent)
        {
            if (throttleOrDynPercent < 0)
            {
                float step = 100 / DynamicBrakeFullRangeDecreaseTimeSeconds * elapsedClockSeconds;
                throttleOrDynPercent = Math.Min(throttleOrDynPercent + step, Math.Min(maxPercent, 0));
            }
            else
            {
                float step = 100 / ThrottleFullRangeIncreaseTimeSeconds * elapsedClockSeconds;
                throttleOrDynPercent = Math.Min(throttleOrDynPercent + step, maxPercent);
            }
        }

        void DecreaseForce(ref float throttleOrDynPercent, float elapsedClockSeconds, float minPercent)
        {
            if (throttleOrDynPercent > 0)
            {
                float step = 100 / ThrottleFullRangeDecreaseTimeSeconds * elapsedClockSeconds;
                throttleOrDynPercent = Math.Max(throttleOrDynPercent - step, Math.Max(minPercent, 0));
            }
            else
            {
                float step = 100 / DynamicBrakeFullRangeIncreaseTimeSeconds * elapsedClockSeconds;
                throttleOrDynPercent = Math.Max(throttleOrDynPercent - step, minPercent);
            }
        }

        void SetTrainBrake(ref float brakePercent, float elapsedClockSeconds, float deltaSpeedMpS)
        {
            if (deltaSpeedMpS > -SpeedDeltaToEnableFullTrainBrake)
            {
                if (!Locomotive.DynamicBrakeAvailable || deltaSpeedMpS < -SpeedDeltaToEnableTrainBrake)
                {
                    CCIsUsingTrainBrake = true;
                    brakePercent = TrainBrakeMinPercentValue - 3.0f + (-deltaSpeedMpS * 10)/SpeedDeltaToEnableTrainBrake;
                }
                else
                {
                    brakePercent = 0;
                }
            }
            else
            {
                CCIsUsingTrainBrake = true;
                if (RelativeAccelerationMpSS > -MaxDecelerationMpSS + 0.01f)
                    brakePercent += 100/TrainBrakeFullRangeIncreaseTimeSeconds*elapsedClockSeconds;
                else if (RelativeAccelerationMpSS < -MaxDecelerationMpSS - 0.01f)
                    brakePercent -= 100/TrainBrakeFullRangeDecreaseTimeSeconds*elapsedClockSeconds;
                brakePercent = MathHelper.Clamp(brakePercent, TrainBrakeMinPercentValue - 3.0f, TrainBrakeMaxPercentValue);
            }
        }

        private float previousSelectedSpeed = 0;
        public float GetDataOf(CabViewControl cvc)
        {
            float data = 0;
            switch (cvc.ControlType)
            {
                case CABViewControlTypes.ORTS_SELECTED_SPEED:
                case CABViewControlTypes.ORTS_SELECTED_SPEED_DISPLAY:
                    bool metric = cvc.Units == CABViewControlUnits.KM_PER_HOUR;
                    float temp = (float)Math.Round(MpS.FromMpS(SetSpeedMpS, metric));
                    if (previousSelectedSpeed < temp) previousSelectedSpeed += 1f;
                    if (previousSelectedSpeed > temp) previousSelectedSpeed -= 1f;
                    data = previousSelectedSpeed;
                    break;
                case CABViewControlTypes.ORTS_SELECTED_SPEED_MODE:
                    data = (float)SpeedSelMode;
                    break;
                case CABViewControlTypes.ORTS_SELECTED_SPEED_REGULATOR_MODE:
                    data = (float)SpeedRegMode;
                    break;
                case CABViewControlTypes.ORTS_SELECTED_SPEED_SELECTOR:
                    metric = cvc.Units == CABViewControlUnits.KM_PER_HOUR;
                    data = (float)Math.Round(MpS.FromMpS(SelectedSpeedMpS, metric));
                    break;
                case CABViewControlTypes.ORTS_SELECTED_SPEED_MAXIMUM_ACCELERATION:
                    if (SpeedRegMode == SpeedRegulatorMode.Auto || MaxForceKeepSelectedStepWhenManualModeSet)
                    {
                        data = SelectedMaxAccelerationStep * (float)cvc.MaxValue / SpeedRegulatorMaxForceSteps;
                    }
                    else
                        data = 0;
                    break;
                case CABViewControlTypes.ORTS_RESTRICTED_SPEED_ZONE_ACTIVE:
                    data = RestrictedRegionOdometer.Started ? 1 : 0;
                    break;
                case CABViewControlTypes.ORTS_NUMBER_OF_AXES_DISPLAY_UNITS:
                    data = SelectedNumberOfAxles % 10;
                    break;
                case CABViewControlTypes.ORTS_NUMBER_OF_AXES_DISPLAY_TENS:
                    data = (SelectedNumberOfAxles / 10) % 10;
                    break;
                case CABViewControlTypes.ORTS_NUMBER_OF_AXES_DISPLAY_HUNDREDS:
                    data = (SelectedNumberOfAxles / 100) % 10;
                    break;
                case CABViewControlTypes.ORTS_TRAIN_LENGTH_METERS:
                    data = TrainLengthMeters;
                    break;
                case CABViewControlTypes.ORTS_REMAINING_TRAIN_LENGHT_SPEED_RESTRICTED:
                    data = RestrictedRegionOdometer.Started ? RestrictedRegionOdometer.RemainingValue : 0;
                    break;
                case CABViewControlTypes.ORTS_REMAINING_TRAIN_LENGTH_PERCENT:
                    if (SpeedRegMode == CruiseControl.SpeedRegulatorMode.Auto && TrainLengthMeters == 0)
                    {
                        data = RestrictedRegionOdometer.Started ? RestrictedRegionOdometer.RemainingValue / TrainLengthMeters : 0;
                    }
                    break;
                case CABViewControlTypes.ORTS_MOTIVE_FORCE:
                    data = Locomotive.FilteredMotiveForceN;
                    break;
                case CABViewControlTypes.ORTS_MOTIVE_FORCE_KILONEWTON:
                    if (Locomotive.FilteredMotiveForceN > Locomotive.DynamicBrakeForceN)
                        data = (float)Math.Round(Locomotive.FilteredMotiveForceN / 1000, 0);
                    else if (Locomotive.DynamicBrakeForceN > 0)
                        data = -(float)Math.Round(Locomotive.DynamicBrakeForceN / 1000, 0);
                    break;
                case CABViewControlTypes.ORTS_MAXIMUM_FORCE:
                    data = Locomotive.MaxForceN;
                    break;
                case CABViewControlTypes.ORTS_FORCE_IN_PERCENT_THROTTLE_AND_DYNAMIC_BRAKE:
                    if (Locomotive.ThrottlePercent > 0)
                    {
                        data = Locomotive.ThrottlePercent;
                    }
                    else if (Locomotive.DynamicBrakePercent > 0 && Locomotive.AbsSpeedMpS > 0)
                    {
                        data = -Locomotive.DynamicBrakePercent;
                    }
                    else data = 0;
                    break;
                case CABViewControlTypes.ORTS_TRAIN_TYPE_PAX_OR_CARGO:
                    data = (int)Locomotive.SelectedTrainType;
                    break;
                case CABViewControlTypes.ORTS_CONTROLLER_VOLTAGE:
                    data = CCThrottleOrDynBrakePercent;
                    break;
                case CABViewControlTypes.ORTS_AMPERS_BY_CONTROLLER_VOLTAGE:
                    if (SpeedRegMode == SpeedRegulatorMode.Auto)
                    {
                        if (CCThrottleOrDynBrakePercent < 0) data = -CCThrottleOrDynBrakePercent / 100 * (Locomotive.MaxCurrentA * 0.8f);
                        else data = CCThrottleOrDynBrakePercent / 100 * (Locomotive.MaxCurrentA * 0.8f);
                        if (data == 0 && Locomotive.DynamicBrakePercent > 0 && Locomotive.AbsSpeedMpS > 0) data = Locomotive.DynamicBrakePercent / 100 * (Locomotive.MaxCurrentA * 0.8f);
                    }
                    else
                    {
                        if (Locomotive.DynamicBrakePercent > 0 && Locomotive.AbsSpeedMpS > 0) data = Locomotive.DynamicBrakePercent / 200 * (Locomotive.MaxCurrentA * 0.8f);
                        else data = Locomotive.ThrottlePercent / 100 * (Locomotive.MaxCurrentA * 0.8f);
                    }
                    break;
                case CABViewControlTypes.ORTS_ACCELERATION_IN_TIME:
                    {
                        data = AccelerationBits;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SELECT_SPEED:
                    data = SelectingSpeedPressed ? 1 : 0;
                    break;
                case CABViewControlTypes.ORTS_CC_SPEED_0:
                    {
                        data = Speed0Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_10:
                    {
                        data = Speed10Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_20:
                    {
                        data = Speed20Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_30:
                    {
                        data = Speed30Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_40:
                    {
                        data = Speed40Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_50:
                    {
                        data = Speed50Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_60:
                    {
                        data = Speed60Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_70:
                    {
                        data = Speed70Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_80:
                    {
                        data = Speed80Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_90:
                    {
                        data = Speed90Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_100:
                    {
                        data = Speed100Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_110:
                    {
                        data = Speed110Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_120:
                    {
                        data = Speed120Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_130:
                    {
                        data = Speed130Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_140:
                    {
                        data = Speed140Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_150:
                    {
                        data = Speed150Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_160:
                    {
                        data = Speed160Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_170:
                    {
                        data = Speed170Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_180:
                    {
                        data = Speed180Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_190:
                    {
                        data = Speed190Pressed ? 1 : 0;
                        break;
                    }
                case CABViewControlTypes.ORTS_CC_SPEED_200:
                    {
                        data = Speed200Pressed ? 1 : 0;
                        break;
                    }
                default:
                    data = 0;
                    break;
            }
            return data;
        }

        public string GetCruiseControlStatus()
        {
            var cruiseControlStatus = SpeedRegMode.ToString();
            return cruiseControlStatus;
        }

        public enum ControllerCruiseControlLogic
        {
            None,
            Full,
            SpeedOnly
        }
    }
}
