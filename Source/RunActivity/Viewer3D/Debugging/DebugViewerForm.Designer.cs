﻿namespace Orts.Viewer3D.Debugging
{
    partial class DispatchViewer
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
             components.Dispose();
             grayPen.Dispose();
             greenPen.Dispose();
             orangePen.Dispose();
             redPen.Dispose();
             pathPen.Dispose();
             trainPen.Dispose();
             trainBrush.Dispose();
             trainFont.Dispose();
             sidingBrush.Dispose();
             sidingFont.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.windowSizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.resLabel = new System.Windows.Forms.Label();
            this.AvatarView = new System.Windows.Forms.ListView();
            this.rmvButton = new System.Windows.Forms.Button();
            this.chkAllowUserSwitch = new System.Windows.Forms.CheckBox();
            this.chkShowAvatars = new System.Windows.Forms.CheckBox();
            this.MSG = new System.Windows.Forms.TextBox();
            this.msgSelected = new System.Windows.Forms.Button();
            this.msgAll = new System.Windows.Forms.Button();
            this.composeMSG = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.reply2Selected = new System.Windows.Forms.Button();
            this.chkDrawPath = new System.Windows.Forms.CheckBox();
            this.boxSetSignal = new System.Windows.Forms.ListBox();
            this.boxSetSwitch = new System.Windows.Forms.ListBox();
            this.chkPickSignals = new System.Windows.Forms.CheckBox();
            this.chkPickSwitches = new System.Windows.Forms.CheckBox();
            this.chkAllowNew = new System.Windows.Forms.CheckBox();
            this.messages = new System.Windows.Forms.ListBox();
            this.btnAssist = new System.Windows.Forms.Button();
            this.btnNormal = new System.Windows.Forms.Button();
            this.btnFollow = new System.Windows.Forms.Button();
            this.chkBoxPenalty = new System.Windows.Forms.CheckBox();
            this.chkPreferGreen = new System.Windows.Forms.CheckBox();
            this.btnSeeInGame = new System.Windows.Forms.Button();
            this.lblSimulationTimeText = new System.Windows.Forms.Label();
            this.lblSimulationTimeValue = new System.Windows.Forms.Label();
            this.lblShow = new System.Windows.Forms.Label();
            this.cbShowPlatforms = new System.Windows.Forms.CheckBox();
            this.cbShowSidings = new System.Windows.Forms.CheckBox();
            this.cbShowSignals = new System.Windows.Forms.CheckBox();
            this.cbSignalState = new System.Windows.Forms.CheckBox();
            this.cbAllTrains = new System.Windows.Forms.CheckBox();
            this.cbActiveTrains = new System.Windows.Forms.CheckBox();
            this.lblDaylightOffsetH = new System.Windows.Forms.Label();
            this.numDaylightOffsetHours = new System.Windows.Forms.NumericUpDown();
            this.bBackgroundColor = new System.Windows.Forms.Button();
            this.cdBackground = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.windowSizeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDaylightOffsetHours)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 131);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(754, 626);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMouseUp);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.Location = new System.Drawing.Point(812, 105);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(93, 23);
            this.refreshButton.TabIndex = 1;
            this.refreshButton.Text = "View Train";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // windowSizeUpDown
            // 
            this.windowSizeUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.windowSizeUpDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowSizeUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.windowSizeUpDown.Location = new System.Drawing.Point(801, 5);
            this.windowSizeUpDown.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.windowSizeUpDown.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.windowSizeUpDown.Name = "windowSizeUpDown";
            this.windowSizeUpDown.Size = new System.Drawing.Size(79, 23);
            this.windowSizeUpDown.TabIndex = 6;
            this.windowSizeUpDown.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.windowSizeUpDown.ValueChanged += new System.EventHandler(this.windowSizeUpDown_ValueChanged);
            // 
            // resLabel
            // 
            this.resLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resLabel.AutoSize = true;
            this.resLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resLabel.Location = new System.Drawing.Point(886, 9);
            this.resLabel.Name = "resLabel";
            this.resLabel.Size = new System.Drawing.Size(19, 16);
            this.resLabel.TabIndex = 8;
            this.resLabel.Text = "m";
            // 
            // AvatarView
            // 
            this.AvatarView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AvatarView.HideSelection = false;
            this.AvatarView.Location = new System.Drawing.Point(773, 160);
            this.AvatarView.Name = "AvatarView";
            this.AvatarView.Size = new System.Drawing.Size(121, 597);
            this.AvatarView.TabIndex = 14;
            this.AvatarView.UseCompatibleStateImageBehavior = false;
            this.AvatarView.SelectedIndexChanged += new System.EventHandler(this.AvatarView_SelectedIndexChanged);
            // 
            // rmvButton
            // 
            this.rmvButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rmvButton.Location = new System.Drawing.Point(760, 132);
            this.rmvButton.Margin = new System.Windows.Forms.Padding(2);
            this.rmvButton.Name = "rmvButton";
            this.rmvButton.Size = new System.Drawing.Size(72, 24);
            this.rmvButton.TabIndex = 15;
            this.rmvButton.Text = "Remove";
            this.rmvButton.UseVisualStyleBackColor = true;
            this.rmvButton.Click += new System.EventHandler(this.rmvButton_Click);
            // 
            // chkAllowUserSwitch
            // 
            this.chkAllowUserSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllowUserSwitch.AutoSize = true;
            this.chkAllowUserSwitch.Checked = true;
            this.chkAllowUserSwitch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllowUserSwitch.Location = new System.Drawing.Point(699, 49);
            this.chkAllowUserSwitch.Name = "chkAllowUserSwitch";
            this.chkAllowUserSwitch.Size = new System.Drawing.Size(83, 17);
            this.chkAllowUserSwitch.TabIndex = 16;
            this.chkAllowUserSwitch.Text = "Auto Switch";
            this.chkAllowUserSwitch.UseVisualStyleBackColor = true;
            this.chkAllowUserSwitch.CheckedChanged += new System.EventHandler(this.chkAllowUserSwitch_CheckedChanged);
            // 
            // chkShowAvatars
            // 
            this.chkShowAvatars.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowAvatars.AutoSize = true;
            this.chkShowAvatars.Checked = true;
            this.chkShowAvatars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowAvatars.Location = new System.Drawing.Point(699, 31);
            this.chkShowAvatars.Name = "chkShowAvatars";
            this.chkShowAvatars.Size = new System.Drawing.Size(92, 17);
            this.chkShowAvatars.TabIndex = 17;
            this.chkShowAvatars.Text = "Show Avatars";
            this.chkShowAvatars.UseVisualStyleBackColor = true;
            this.chkShowAvatars.CheckedChanged += new System.EventHandler(this.chkShowAvatars_CheckedChanged);
            // 
            // MSG
            // 
            this.MSG.Enabled = false;
            this.MSG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MSG.Location = new System.Drawing.Point(1, 3);
            this.MSG.Name = "MSG";
            this.MSG.Size = new System.Drawing.Size(583, 26);
            this.MSG.TabIndex = 18;
            this.MSG.WordWrap = false;
            this.MSG.Enter += new System.EventHandler(this.MSGEnter);
            this.MSG.Leave += new System.EventHandler(this.MSGLeave);
            this.MSG.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.checkKeys);
            // 
            // msgSelected
            // 
            this.msgSelected.Enabled = false;
            this.msgSelected.Location = new System.Drawing.Point(590, 61);
            this.msgSelected.Margin = new System.Windows.Forms.Padding(2);
            this.msgSelected.MaximumSize = new System.Drawing.Size(200, 24);
            this.msgSelected.MinimumSize = new System.Drawing.Size(104, 24);
            this.msgSelected.Name = "msgSelected";
            this.msgSelected.Size = new System.Drawing.Size(104, 24);
            this.msgSelected.TabIndex = 19;
            this.msgSelected.Text = "MSG to Selected";
            this.msgSelected.UseVisualStyleBackColor = true;
            this.msgSelected.Click += new System.EventHandler(this.msgSelected_Click);
            // 
            // msgAll
            // 
            this.msgAll.Enabled = false;
            this.msgAll.Location = new System.Drawing.Point(590, 32);
            this.msgAll.Margin = new System.Windows.Forms.Padding(2);
            this.msgAll.MaximumSize = new System.Drawing.Size(200, 24);
            this.msgAll.MinimumSize = new System.Drawing.Size(104, 24);
            this.msgAll.Name = "msgAll";
            this.msgAll.Size = new System.Drawing.Size(104, 24);
            this.msgAll.TabIndex = 20;
            this.msgAll.Text = "MSG to All";
            this.msgAll.UseVisualStyleBackColor = true;
            this.msgAll.Click += new System.EventHandler(this.msgAll_Click);
            // 
            // composeMSG
            // 
            this.composeMSG.Location = new System.Drawing.Point(590, 3);
            this.composeMSG.Margin = new System.Windows.Forms.Padding(2);
            this.composeMSG.MaximumSize = new System.Drawing.Size(200, 24);
            this.composeMSG.MinimumSize = new System.Drawing.Size(104, 24);
            this.composeMSG.Name = "composeMSG";
            this.composeMSG.Size = new System.Drawing.Size(104, 24);
            this.composeMSG.TabIndex = 21;
            this.composeMSG.Text = "Compose MSG";
            this.composeMSG.UseVisualStyleBackColor = true;
            this.composeMSG.Click += new System.EventHandler(this.composeMSG_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(769, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Res";
            // 
            // reply2Selected
            // 
            this.reply2Selected.Enabled = false;
            this.reply2Selected.Location = new System.Drawing.Point(590, 90);
            this.reply2Selected.Margin = new System.Windows.Forms.Padding(2);
            this.reply2Selected.MaximumSize = new System.Drawing.Size(200, 24);
            this.reply2Selected.MinimumSize = new System.Drawing.Size(104, 24);
            this.reply2Selected.Name = "reply2Selected";
            this.reply2Selected.Size = new System.Drawing.Size(104, 24);
            this.reply2Selected.TabIndex = 23;
            this.reply2Selected.Text = "Reply to Selected";
            this.reply2Selected.UseVisualStyleBackColor = true;
            this.reply2Selected.Click += new System.EventHandler(this.replySelected);
            // 
            // chkDrawPath
            // 
            this.chkDrawPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDrawPath.AutoSize = true;
            this.chkDrawPath.Checked = true;
            this.chkDrawPath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawPath.Location = new System.Drawing.Point(796, 32);
            this.chkDrawPath.Name = "chkDrawPath";
            this.chkDrawPath.Size = new System.Drawing.Size(76, 17);
            this.chkDrawPath.TabIndex = 24;
            this.chkDrawPath.Text = "Draw Path";
            this.chkDrawPath.UseVisualStyleBackColor = true;
            this.chkDrawPath.CheckedChanged += new System.EventHandler(this.chkDrawPathChanged);
            // 
            // boxSetSignal
            // 
            this.boxSetSignal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.boxSetSignal.Enabled = false;
            this.boxSetSignal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxSetSignal.FormattingEnabled = true;
            this.boxSetSignal.ItemHeight = 20;
            this.boxSetSignal.Location = new System.Drawing.Point(209, 205);
            this.boxSetSignal.MinimumSize = new System.Drawing.Size(160, 100);
            this.boxSetSignal.Name = "boxSetSignal";
            this.boxSetSignal.Size = new System.Drawing.Size(164, 100);
            this.boxSetSignal.TabIndex = 25;
            this.boxSetSignal.Visible = false;
            this.boxSetSignal.SelectedIndexChanged += new System.EventHandler(this.boxSetSignalChosen);
            // 
            // boxSetSwitch
            // 
            this.boxSetSwitch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.boxSetSwitch.Enabled = false;
            this.boxSetSwitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxSetSwitch.FormattingEnabled = true;
            this.boxSetSwitch.ItemHeight = 20;
            this.boxSetSwitch.Items.AddRange(new object[] {
            "To Main Route",
            "To Side Route"});
            this.boxSetSwitch.Location = new System.Drawing.Point(398, 205);
            this.boxSetSwitch.MinimumSize = new System.Drawing.Size(120, 50);
            this.boxSetSwitch.Name = "boxSetSwitch";
            this.boxSetSwitch.Size = new System.Drawing.Size(125, 40);
            this.boxSetSwitch.TabIndex = 26;
            this.boxSetSwitch.Visible = false;
            this.boxSetSwitch.SelectedIndexChanged += new System.EventHandler(this.boxSetSwitchChosen);
            // 
            // chkPickSignals
            // 
            this.chkPickSignals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPickSignals.AutoSize = true;
            this.chkPickSignals.Checked = true;
            this.chkPickSignals.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPickSignals.Location = new System.Drawing.Point(796, 49);
            this.chkPickSignals.Name = "chkPickSignals";
            this.chkPickSignals.Size = new System.Drawing.Size(84, 17);
            this.chkPickSignals.TabIndex = 27;
            this.chkPickSignals.Text = "Pick Signals";
            this.chkPickSignals.UseVisualStyleBackColor = true;
            // 
            // chkPickSwitches
            // 
            this.chkPickSwitches.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPickSwitches.AutoSize = true;
            this.chkPickSwitches.Checked = true;
            this.chkPickSwitches.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPickSwitches.Location = new System.Drawing.Point(796, 66);
            this.chkPickSwitches.Name = "chkPickSwitches";
            this.chkPickSwitches.Size = new System.Drawing.Size(93, 17);
            this.chkPickSwitches.TabIndex = 28;
            this.chkPickSwitches.Text = "Pick Switches";
            this.chkPickSwitches.UseVisualStyleBackColor = true;
            // 
            // chkAllowNew
            // 
            this.chkAllowNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllowNew.AutoSize = true;
            this.chkAllowNew.Checked = true;
            this.chkAllowNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllowNew.Location = new System.Drawing.Point(699, 12);
            this.chkAllowNew.Name = "chkAllowNew";
            this.chkAllowNew.Size = new System.Drawing.Size(67, 17);
            this.chkAllowNew.TabIndex = 29;
            this.chkAllowNew.Text = "Can Join";
            this.chkAllowNew.UseVisualStyleBackColor = true;
            this.chkAllowNew.CheckedChanged += new System.EventHandler(this.chkAllowNewCheck);
            // 
            // messages
            // 
            this.messages.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messages.FormattingEnabled = true;
            this.messages.ItemHeight = 18;
            this.messages.Location = new System.Drawing.Point(1, 38);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(583, 58);
            this.messages.TabIndex = 22;
            this.messages.SelectedIndexChanged += new System.EventHandler(this.msgSelectedChanged);
            // 
            // btnAssist
            // 
            this.btnAssist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAssist.Location = new System.Drawing.Point(691, 104);
            this.btnAssist.Margin = new System.Windows.Forms.Padding(2);
            this.btnAssist.Name = "btnAssist";
            this.btnAssist.Size = new System.Drawing.Size(48, 24);
            this.btnAssist.TabIndex = 30;
            this.btnAssist.Text = "Assist";
            this.btnAssist.UseVisualStyleBackColor = true;
            this.btnAssist.Click += new System.EventHandler(this.AssistClick);
            // 
            // btnNormal
            // 
            this.btnNormal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNormal.Location = new System.Drawing.Point(749, 104);
            this.btnNormal.Margin = new System.Windows.Forms.Padding(2);
            this.btnNormal.Name = "btnNormal";
            this.btnNormal.Size = new System.Drawing.Size(58, 24);
            this.btnNormal.TabIndex = 31;
            this.btnNormal.Text = "Normal";
            this.btnNormal.UseVisualStyleBackColor = true;
            this.btnNormal.Click += new System.EventHandler(this.btnNormalClick);
            // 
            // btnFollow
            // 
            this.btnFollow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFollow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFollow.Location = new System.Drawing.Point(839, 133);
            this.btnFollow.Name = "btnFollow";
            this.btnFollow.Size = new System.Drawing.Size(60, 23);
            this.btnFollow.TabIndex = 32;
            this.btnFollow.Text = "Follow";
            this.btnFollow.UseVisualStyleBackColor = true;
            this.btnFollow.Click += new System.EventHandler(this.btnFollowClick);
            // 
            // chkBoxPenalty
            // 
            this.chkBoxPenalty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBoxPenalty.AutoSize = true;
            this.chkBoxPenalty.Checked = true;
            this.chkBoxPenalty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxPenalty.Location = new System.Drawing.Point(699, 66);
            this.chkBoxPenalty.Name = "chkBoxPenalty";
            this.chkBoxPenalty.Size = new System.Drawing.Size(61, 17);
            this.chkBoxPenalty.TabIndex = 33;
            this.chkBoxPenalty.Text = "Penalty";
            this.chkBoxPenalty.UseVisualStyleBackColor = true;
            this.chkBoxPenalty.CheckedChanged += new System.EventHandler(this.chkOPenaltyHandle);
            // 
            // chkPreferGreen
            // 
            this.chkPreferGreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPreferGreen.AutoSize = true;
            this.chkPreferGreen.Checked = true;
            this.chkPreferGreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreferGreen.Location = new System.Drawing.Point(699, 83);
            this.chkPreferGreen.Name = "chkPreferGreen";
            this.chkPreferGreen.Size = new System.Drawing.Size(86, 17);
            this.chkPreferGreen.TabIndex = 34;
            this.chkPreferGreen.Text = "Prefer Green";
            this.chkPreferGreen.UseVisualStyleBackColor = true;
            this.chkPreferGreen.Visible = false;
            this.chkPreferGreen.CheckedChanged += new System.EventHandler(this.chkPreferGreenHandle);
            // 
            // btnSeeInGame
            // 
            this.btnSeeInGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeeInGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeeInGame.Location = new System.Drawing.Point(812, 81);
            this.btnSeeInGame.Name = "btnSeeInGame";
            this.btnSeeInGame.Size = new System.Drawing.Size(93, 23);
            this.btnSeeInGame.TabIndex = 35;
            this.btnSeeInGame.Text = "See in Game";
            this.btnSeeInGame.UseVisualStyleBackColor = true;
            this.btnSeeInGame.Click += new System.EventHandler(this.btnSeeInGameClick);
            // 
            // lblSimulationTimeText
            // 
            this.lblSimulationTimeText.AutoSize = true;
            this.lblSimulationTimeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimulationTimeText.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSimulationTimeText.Location = new System.Drawing.Point(5, 6);
            this.lblSimulationTimeText.Name = "lblSimulationTimeText";
            this.lblSimulationTimeText.Size = new System.Drawing.Size(104, 16);
            this.lblSimulationTimeText.TabIndex = 36;
            this.lblSimulationTimeText.Text = "Simulation Time";
            this.lblSimulationTimeText.Visible = false;
            // 
            // lblSimulationTimeValue
            // 
            this.lblSimulationTimeValue.AutoSize = true;
            this.lblSimulationTimeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimulationTimeValue.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblSimulationTimeValue.Location = new System.Drawing.Point(121, 6);
            this.lblSimulationTimeValue.Name = "lblSimulationTimeValue";
            this.lblSimulationTimeValue.Size = new System.Drawing.Size(104, 16);
            this.lblSimulationTimeValue.TabIndex = 37;
            this.lblSimulationTimeValue.Text = "Simulation Time";
            this.lblSimulationTimeValue.Visible = false;
            // 
            // lblShow
            // 
            this.lblShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShow.AutoSize = true;
            this.lblShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShow.Location = new System.Drawing.Point(775, 173);
            this.lblShow.Name = "lblShow";
            this.lblShow.Size = new System.Drawing.Size(38, 15);
            this.lblShow.TabIndex = 38;
            this.lblShow.Text = "Show";
            this.lblShow.Visible = false;
            // 
            // cbShowPlatforms
            // 
            this.cbShowPlatforms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShowPlatforms.AutoSize = true;
            this.cbShowPlatforms.Checked = true;
            this.cbShowPlatforms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowPlatforms.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowPlatforms.Location = new System.Drawing.Point(778, 195);
            this.cbShowPlatforms.Name = "cbShowPlatforms";
            this.cbShowPlatforms.Size = new System.Drawing.Size(78, 19);
            this.cbShowPlatforms.TabIndex = 39;
            this.cbShowPlatforms.Text = "Platforms";
            this.cbShowPlatforms.UseVisualStyleBackColor = true;
            this.cbShowPlatforms.Visible = false;
            // 
            // cbShowSidings
            // 
            this.cbShowSidings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShowSidings.AutoSize = true;
            this.cbShowSidings.Checked = true;
            this.cbShowSidings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowSidings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowSidings.Location = new System.Drawing.Point(778, 220);
            this.cbShowSidings.Name = "cbShowSidings";
            this.cbShowSidings.Size = new System.Drawing.Size(67, 19);
            this.cbShowSidings.TabIndex = 40;
            this.cbShowSidings.Text = "Sidings";
            this.cbShowSidings.UseVisualStyleBackColor = true;
            this.cbShowSidings.Visible = false;
            // 
            // cbShowSignals
            // 
            this.cbShowSignals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShowSignals.AutoSize = true;
            this.cbShowSignals.Checked = true;
            this.cbShowSignals.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowSignals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowSignals.Location = new System.Drawing.Point(778, 245);
            this.cbShowSignals.Name = "cbShowSignals";
            this.cbShowSignals.Size = new System.Drawing.Size(67, 19);
            this.cbShowSignals.TabIndex = 41;
            this.cbShowSignals.Text = "Signals";
            this.cbShowSignals.UseVisualStyleBackColor = true;
            this.cbShowSignals.Visible = false;
            // 
            // cbSignalState
            // 
            this.cbSignalState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSignalState.AutoSize = true;
            this.cbSignalState.Checked = true;
            this.cbSignalState.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSignalState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSignalState.Location = new System.Drawing.Point(778, 270);
            this.cbSignalState.Name = "cbSignalState";
            this.cbSignalState.Size = new System.Drawing.Size(92, 19);
            this.cbSignalState.TabIndex = 42;
            this.cbSignalState.Text = "Signal State";
            this.cbSignalState.UseVisualStyleBackColor = true;
            this.cbSignalState.Visible = false;
            // 
            // cbAllTrains
            // 
            this.cbAllTrains.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAllTrains.AutoSize = true;
            this.cbAllTrains.Checked = true;
            this.cbAllTrains.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllTrains.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAllTrains.Location = new System.Drawing.Point(778, 295);
            this.cbAllTrains.Name = "cbAllTrains";
            this.cbAllTrains.Size = new System.Drawing.Size(76, 19);
            this.cbAllTrains.TabIndex = 43;
            this.cbAllTrains.Text = "All Trains";
            this.cbAllTrains.UseVisualStyleBackColor = true;
            this.cbAllTrains.Visible = false;
            // 
            // cbActiveTrains
            // 
            this.cbActiveTrains.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbActiveTrains.AutoSize = true;
            this.cbActiveTrains.Checked = true;
            this.cbActiveTrains.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActiveTrains.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbActiveTrains.Location = new System.Drawing.Point(778, 320);
            this.cbActiveTrains.Name = "cbActiveTrains";
            this.cbActiveTrains.Size = new System.Drawing.Size(94, 19);
            this.cbActiveTrains.TabIndex = 44;
            this.cbActiveTrains.Text = "Active Trains";
            this.cbActiveTrains.UseVisualStyleBackColor = true;
            this.cbActiveTrains.Visible = false;
            // 
            // lblDaylightOffsetH
            // 
            this.lblDaylightOffsetH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDaylightOffsetH.AutoSize = true;
            this.lblDaylightOffsetH.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaylightOffsetH.Location = new System.Drawing.Point(775, 356);
            this.lblDaylightOffsetH.Name = "lblDaylightOffsetH";
            this.lblDaylightOffsetH.Size = new System.Drawing.Size(113, 15);
            this.lblDaylightOffsetH.TabIndex = 45;
            this.lblDaylightOffsetH.Text = "Daylight Offset (hrs)";
            this.lblDaylightOffsetH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDaylightOffsetH.Visible = false;
            // 
            // numDaylightOffsetHours
            // 
            this.numDaylightOffsetHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numDaylightOffsetHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDaylightOffsetHours.Location = new System.Drawing.Point(778, 376);
            this.numDaylightOffsetHours.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numDaylightOffsetHours.Name = "numDaylightOffsetHours";
            this.numDaylightOffsetHours.Size = new System.Drawing.Size(49, 21);
            this.numDaylightOffsetHours.TabIndex = 46;
            this.numDaylightOffsetHours.Visible = false;
            // 
            // bBackgroundColor
            // 
            this.bBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bBackgroundColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bBackgroundColor.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.bBackgroundColor.Location = new System.Drawing.Point(778, 414);
            this.bBackgroundColor.Name = "bBackgroundColor";
            this.bBackgroundColor.Size = new System.Drawing.Size(115, 30);
            this.bBackgroundColor.TabIndex = 47;
            this.bBackgroundColor.Text = "Background Color";
            this.bBackgroundColor.UseVisualStyleBackColor = true;
            this.bBackgroundColor.Visible = false;
            // 
            // cdBackground
            // 
            this.cdBackground.AnyColor = true;
            this.cdBackground.ShowHelp = true;
            // 
            // DispatchViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(923, 768);
            this.Controls.Add(this.bBackgroundColor);
            this.Controls.Add(this.numDaylightOffsetHours);
            this.Controls.Add(this.lblDaylightOffsetH);
            this.Controls.Add(this.cbActiveTrains);
            this.Controls.Add(this.cbAllTrains);
            this.Controls.Add(this.cbSignalState);
            this.Controls.Add(this.cbShowSignals);
            this.Controls.Add(this.cbShowSidings);
            this.Controls.Add(this.cbShowPlatforms);
            this.Controls.Add(this.lblShow);
            this.Controls.Add(this.lblSimulationTimeValue);
            this.Controls.Add(this.lblSimulationTimeText);
            this.Controls.Add(this.btnSeeInGame);
            this.Controls.Add(this.chkPreferGreen);
            this.Controls.Add(this.chkBoxPenalty);
            this.Controls.Add(this.btnFollow);
            this.Controls.Add(this.btnNormal);
            this.Controls.Add(this.btnAssist);
            this.Controls.Add(this.chkAllowNew);
            this.Controls.Add(this.chkPickSwitches);
            this.Controls.Add(this.chkPickSignals);
            this.Controls.Add(this.boxSetSwitch);
            this.Controls.Add(this.boxSetSignal);
            this.Controls.Add(this.chkDrawPath);
            this.Controls.Add(this.reply2Selected);
            this.Controls.Add(this.messages);
            this.Controls.Add(this.composeMSG);
            this.Controls.Add(this.msgAll);
            this.Controls.Add(this.msgSelected);
            this.Controls.Add(this.MSG);
            this.Controls.Add(this.chkShowAvatars);
            this.Controls.Add(this.chkAllowUserSwitch);
            this.Controls.Add(this.rmvButton);
            this.Controls.Add(this.AvatarView);
            this.Controls.Add(this.resLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.windowSizeUpDown);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "DispatchViewer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Dispatch WIndow";
            this.Leave += new System.EventHandler(this.DispatcherLeave);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.windowSizeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDaylightOffsetHours)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.PictureBox pictureBox1;
	  private System.Windows.Forms.Button refreshButton;
	  private System.Windows.Forms.NumericUpDown windowSizeUpDown;
	  private System.Windows.Forms.Label resLabel;
	  private System.Windows.Forms.ListView AvatarView;
	  private System.Windows.Forms.Button rmvButton;
	  private System.Windows.Forms.CheckBox chkAllowUserSwitch;
	  private System.Windows.Forms.CheckBox chkShowAvatars;
	  private System.Windows.Forms.TextBox MSG;
	  private System.Windows.Forms.Button msgSelected;
	  private System.Windows.Forms.Button msgAll;
	  private System.Windows.Forms.Button composeMSG;
	  private System.Windows.Forms.Label label1;
	  private System.Windows.Forms.Button reply2Selected;
	  private System.Windows.Forms.CheckBox chkDrawPath;
	  private System.Windows.Forms.ListBox boxSetSignal;
	  private System.Windows.Forms.ListBox boxSetSwitch;
	  private System.Windows.Forms.CheckBox chkPickSignals;
	  private System.Windows.Forms.CheckBox chkPickSwitches;
	  private System.Windows.Forms.CheckBox chkAllowNew;
	  private System.Windows.Forms.ListBox messages;
	  private System.Windows.Forms.Button btnAssist;
	  private System.Windows.Forms.Button btnNormal;
	  private System.Windows.Forms.Button btnFollow;
	  private System.Windows.Forms.CheckBox chkBoxPenalty;
	  private System.Windows.Forms.CheckBox chkPreferGreen;
	  private System.Windows.Forms.Button btnSeeInGame;
        private System.Windows.Forms.Label lblSimulationTimeText;
        private System.Windows.Forms.Label lblSimulationTimeValue;
        private System.Windows.Forms.Label lblShow;
        private System.Windows.Forms.CheckBox cbShowPlatforms;
        private System.Windows.Forms.CheckBox cbShowSidings;
        private System.Windows.Forms.CheckBox cbShowSignals;
        private System.Windows.Forms.CheckBox cbSignalState;
        private System.Windows.Forms.CheckBox cbAllTrains;
        private System.Windows.Forms.CheckBox cbActiveTrains;
        private System.Windows.Forms.Label lblDaylightOffsetH;
        public System.Windows.Forms.NumericUpDown numDaylightOffsetHours;
        private System.Windows.Forms.Button bBackgroundColor;
        private System.Windows.Forms.ColorDialog cdBackground;
    }
}
