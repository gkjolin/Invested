﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Enumerations for settings
public enum ForceDisplayUnits { Newtons, Gs }
public enum InterfaceComplexity { Simple, Sums }


public class SettingsMenu : MonoBehaviour {

    // String constants for button text
    // Gameplay
    private const string s_mk45 = "Mouse and Keyboard\n(MB 4 & 5)";
    private const string s_mkQE = "Mouse and Keyboard\n(Keys Q & E)";
    private const string s_game = "Gamepad";
    private const string s_gameDetails = "Disconnect and reconnect gamepad if not working.";
    private const string s_disabled = "Disabled";
    private const string s_enabled = "Enabled";
    private const string s_forc = "Control Force Magntitude";
    private const string s_forcDetails = "Player sets a target force magnitude. Pushes will always try to have that magnitude.";
    private const string s_perc = "Control Force Percentage";
    private const string s_percDetails = "Player sets a percentage of their maximum possible force to push with.";

    // Interface
    private const string s_newt = "Newtons";
    private const string s_newtDetails = "Pushes/pulls will be expressed as forces in units of Newtons.";
    private const string s_grav = "G's";
    private const string s_gravDetails = "Pushes/pulls will be expressed as accelerations in units of G's.";
    private const string s_interfaceSimple = "Only Net";
    private const string s_interfaceSimpleDetails = "HUD will only display net forces on the player and targets.";
    private const string s_interfaceSums = "Sums";
    private const string s_interfaceSumsDetails = "HUD will display net forces as well as each individual Allomantic Force and Anchor Push Boost.";
    private const string s_targetForcesEnabled = "Enabled";
    private const string s_targetForcesEnabledDetails = "Targets will display force(s) acting on them.";
    private const string s_targetForcesDisabled = "Disabled";
    private const string s_targetForcesDisabledDetails = "Targets will not display force(s) acting on them.";
    private const string s_targetMassesEnabled = "Enabled";
    private const string s_targetMassesEnabledDetails = "Highlighted metals will display their mass.";
    private const string s_targetMassesDisabled = "Disabled";
    private const string s_targetMassesDisabledDetails = "Highlighted metals will not display their mass.";

    // Physics
    private const string s_norm = "Allomantic Normal Force\n(ANF)";
    private const string s_normDetails = "Push on an anchored coin. The coin pushes on the ground. The ground pushes back on the coin. That force with which the ground resists your push is returned to you.";
    private const string s_expV = "Exponential with Velocity\n(EWV)";
    private const string s_expVDetails = "AF ∝ e ^ -v/V where v = velocity of Allomancor or target; V = Exponential Constant";
    private const string s_anchorDisabled = "Disabled";
    private const string s_anchorDisabledDetails = "Anchors will provide no stronger push than unanchored targets.";
    private const string s_aNFMinZero = "Zero";
    private const string s_aNFMinZeroDetails = "ANF will never be negative relative to the AF. Realistic behavior.";
    private const string s_aNFMinZeroNegate = "Zero & Negate";
    private const string s_aNFMinZeroNegateDetails = "ANF cannot be negative, but values that would be negative have their sign swapped and improve your push instead. Unrealistic behavior.";
    private const string s_aNFMinDisabled = "Disabled";
    private const string s_aNFMinDisabledDetails = "ANF can be negative. Unrealistic behavior. You can push but actually move towards your target, if your target resists you really well.";
    private const string s_aNFMaxAF = "Allomantic Force";
    private const string s_aNFMaxAFDetails = "ANF will never be higher than the AF. Realistic behavior. You cannot be resisted harder than you can push.";
    private const string s_aNFMaxDisabled = "Disabled";
    private const string s_aNFMaxDisabledDetails = "ANF is uncapped. You can be resisted more than you can push. Sometimes unrealistic behavior.";
    private const string s_aNFEqualityEqual = "Equal";
    private const string s_aNFEqualityEqualDetails = "The ANF on the target and Allomancer will be equal for both, calculated from whichever is more anchored.";
    private const string s_aNFEqualityUnequal = "Unequal";
    private const string s_aNFEqualityUnequalDetails = "The ANFs for the target and Allomancer will be calculated independently, depending on how each is individually anchored.";
    private const string s_eWVAlwaysDecreasing = "No Changes";
    private const string s_eWVAlwaysDecreasingDetails = "The higher the speed of the target, the weaker the push or pull. Period. Realistic behavior.";
    private const string s_eWVOnlyBackwardsDecreases = "Only When Moving Away";
    private const string s_eWVOnlyBackwardsDecreasesDetails = "If a target is moving away from you, the force is weaker. If a target is moving towards you, the force is unaffected.";
    private const string s_eWVChangesWithSign = "Symmetrical";
    private const string s_eWVChangesWithSignDetails = "The faster a target is moving away from you, the weaker the push/pull. The faster a targed is moving towards you, the stronger the push/pull.";
    private const string s_eWVRelative = "Relative";
    private const string s_eWVRelativeDetails = "v = Relative velocity of the target and Allomancer. The net forces on the target and Allomancer are equal.";
    private const string s_eWVAbsolute = "Absolute";
    private const string s_eWVAbsoluteDetails = "v = Absolute velocities of the target and Allomancer. The net forces on the target and Allomancer will be unequal and dependent on their individual velocities.";
    private const string s_inverse = "Inverse Square";
    private const string s_inverseDetails = "AF ∝ 1 / d² where d = distance between Allomancer and target";
    private const string s_linear = "Linear";
    private const string s_linearDetails = "AF ∝ 1 - d / R where d = distance between Allomancer and target; R = maximum range of push";
    private const string s_eWD = "Exponential with Distance";
    private const string s_eWDDetails = "AF ∝ e ^ -d/D where d = distance between Allomancer and target; D = Exponential Constant";
    private const string s_alloConstantDetails = "All pushes/pulls are proportional to this constant.";
    private const string s_maxRDetails = "Nearby metals can be detected within this range.";

    public static ForceDisplayUnits displayUnits = ForceDisplayUnits.Newtons;
    public static InterfaceComplexity interfaceComplexity = InterfaceComplexity.Sums;
    public static bool interfaceTargetForces = true;
    public static bool interfaceTargetMasses = true;

    public bool IsOpen {
        get {
            return gameObject.activeSelf;
        }
    }
    public bool IsGameplayOpen {
        get {
            return gameplayHeader.gameObject.activeSelf;
        }
    }
    public bool IsPhysicsOpen {
        get {
            return physicsHeader.gameObject.activeSelf;
        }
    }
    public bool IsInterfaceOpen {
        get {
            return interfaceHeader.gameObject.activeSelf;
        }
    }

    private Button gameplayButton;
    private Button physicsButton;
    private Button interfaceButton;
    private Transform settingsHeader;
    private Transform gameplayHeader;
    private Transform physicsHeader;
    private Transform interfaceHeader;

    private Button controlSchemeButton;
    private Text controlSchemeButtonText;
    private Text controlSchemeDetails;
    private Text rumbleLabel;
    private Button rumbleButton;
    private Text rumbleButtonText;
    private Button pushControlStyleButton;
    private Text pushControlStyleButtonText;
    private Text pushControlStyleDetails;
    private Slider sensitivity;
    private Text sensitivityValueText;
    private Slider smoothing;
    private Text smoothingValueText;

    private Button forceUnitsButton;
    private Text forceUnitsButtonText;
    private Text forceUnitsDetails;
    private Button interfaceComplexityButton;
    private Text interfaceComplexityButtonText;
    private Text interfaceComplexityDetails;
    private Button targetForcesButton;
    private Text targetForcesButtonText;
    private Text targetForcesDetails;
    private Button targetMassesButton;
    private Text targetMassesButtonText;
    private Text targetMassesDetails;

    private Button closeButton;
    private Button anchoredBoostButton;
    private Text anchoredBoostButtonText;
    private Text anchoredBoostDetails;
    private Button aNFMinimumButton;
    private Text aNFMinimumLabel;
    private Text aNFMinimumButtonText;
    private Text aNFMinimumDetails;
    private Text aNFMaximumLabel;
    private Button aNFMaximumButton;
    private Text aNFMaximumButtonText;
    private Text aNFMaximumDetails;
    private Text eWVSignageLabel;
    private Button eWVSignageButton;
    private Text eWVSignageButtonText;
    private Text eWVSignageDetails;
    private Text eWVRelativityLabel;
    private Button eWVRelativityButton;
    private Text eWVRelativityButtonText;
    private Text eWVRelativityDetails;
    private Text velocityConstantLabel;
    private Slider velocityConstantSlider;
    private Text velocityConstantValueText;
    private Button distanceRelationshipButton;
    private Text distanceRelationshipButtonText;
    private Text distanceRelationshipDetails;
    private Text distanceConstantLabel;
    private Slider distanceConstantSlider;
    private Text distanceConstantValueText;
    private Slider forceConstantSlider;
    private Text forceConstantValueText;
    private Text forceConstantDetails;
    private Slider maxRangeSlider;
    private Text maxRangeValueText;
    private Text maxRangeDetails;



    void Start() {

        // Settings Header
        settingsHeader = transform.GetChild(1);
        Button[] settingsHeaderButtons = settingsHeader.GetComponentsInChildren<Button>();
        physicsButton = settingsHeaderButtons[0];
        gameplayButton = settingsHeaderButtons[1];
        interfaceButton = settingsHeaderButtons[2];
        closeButton = settingsHeaderButtons[3];

        // Physics Header
        physicsHeader = transform.GetChild(2);
        anchoredBoostButton = physicsHeader.GetChild(0).GetChild(0).GetComponent<Button>();
        anchoredBoostButtonText = anchoredBoostButton.transform.GetChild(0).GetComponent<Text>();
        anchoredBoostDetails = anchoredBoostButton.transform.GetChild(1).GetComponent<Text>();

        aNFMinimumLabel = physicsHeader.GetChild(1).GetComponent<Text>();
        aNFMinimumButton = aNFMinimumLabel.GetComponentInChildren<Button>();
        aNFMinimumButtonText = aNFMinimumButton.transform.GetChild(0).GetComponent<Text>();
        aNFMinimumDetails = aNFMinimumButton.transform.GetChild(1).GetComponent<Text>();

        aNFMaximumLabel = physicsHeader.GetChild(2).GetComponent<Text>();
        aNFMaximumButton = aNFMaximumLabel.GetComponentInChildren<Button>();
        aNFMaximumButtonText = aNFMaximumButton.transform.GetChild(0).GetComponent<Text>();
        aNFMaximumDetails = aNFMaximumButton.transform.GetChild(1).GetComponent<Text>();

        eWVSignageLabel = physicsHeader.GetChild(3).GetComponent<Text>();
        eWVSignageButton = eWVSignageLabel.GetComponentInChildren<Button>();
        eWVSignageButtonText = eWVSignageButton.transform.GetChild(0).GetComponent<Text>();
        eWVSignageDetails = eWVSignageButton.transform.GetChild(1).GetComponent<Text>();

        eWVRelativityLabel = physicsHeader.GetChild(4).GetComponent<Text>();
        eWVRelativityButton = eWVRelativityLabel.GetComponentInChildren<Button>();
        eWVRelativityButtonText = eWVRelativityButton.transform.GetChild(0).GetComponent<Text>();
        eWVRelativityDetails = eWVRelativityButton.transform.GetChild(1).GetComponent<Text>();

        velocityConstantLabel = physicsHeader.GetChild(5).GetComponent<Text>();
        velocityConstantSlider = velocityConstantLabel.GetComponentInChildren<Slider>();
        velocityConstantValueText = velocityConstantSlider.GetComponentInChildren<Text>();

        distanceRelationshipButton = physicsHeader.GetChild(6).GetChild(0).GetComponent<Button>();
        distanceRelationshipButtonText = distanceRelationshipButton.GetComponentInChildren<Text>();
        distanceRelationshipDetails = distanceRelationshipButton.transform.GetChild(1).GetComponent<Text>();

        distanceConstantLabel = physicsHeader.GetChild(7).GetComponent<Text>();
        distanceConstantSlider = distanceConstantLabel.GetComponentInChildren<Slider>();
        distanceConstantValueText = distanceConstantSlider.GetComponentInChildren<Text>();

        forceConstantSlider = physicsHeader.GetChild(8).GetComponentInChildren<Slider>();
        forceConstantValueText = forceConstantSlider.GetComponentInChildren<Text>();
        forceConstantDetails = forceConstantSlider.transform.GetChild(4).GetComponent<Text>();
        maxRangeSlider = physicsHeader.GetChild(9).GetComponentInChildren<Slider>();
        maxRangeValueText = maxRangeSlider.GetComponentInChildren<Text>();
        maxRangeDetails = maxRangeSlider.transform.GetChild(4).GetComponent<Text>();

        // Gameplay Header
        gameplayHeader = transform.GetChild(3);
        controlSchemeButton = gameplayHeader.GetChild(0).GetChild(0).GetComponent<Button>();
        controlSchemeButtonText = controlSchemeButton.GetComponentInChildren<Text>();
        controlSchemeDetails = controlSchemeButton.transform.GetChild(1).GetComponent<Text>();

        rumbleLabel = gameplayHeader.GetChild(1).GetComponent<Text>();
        rumbleButton = rumbleLabel.GetComponentInChildren<Button>();
        rumbleButtonText = rumbleButton.GetComponentInChildren<Text>();

        pushControlStyleButton = gameplayHeader.GetChild(2).GetChild(0).GetComponent<Button>();
        pushControlStyleButtonText = pushControlStyleButton.transform.GetChild(0).GetComponent<Text>();
        pushControlStyleDetails = pushControlStyleButton.transform.GetChild(1).GetComponent<Text>();

        sensitivity = gameplayHeader.GetChild(3).GetComponentInChildren<Slider>();
        sensitivityValueText = sensitivity.GetComponentInChildren<Text>();

        smoothing = gameplayHeader.GetChild(4).GetComponentInChildren<Slider>();
        smoothingValueText = smoothing.GetComponentInChildren<Text>();

        // Interface Header
        interfaceHeader = transform.GetChild(4);
        forceUnitsButton = interfaceHeader.GetChild(0).GetChild(0).GetComponent<Button>();
        forceUnitsButtonText = forceUnitsButton.GetComponentInChildren<Text>();
        forceUnitsDetails = forceUnitsButton.transform.GetChild(1).GetComponent<Text>();

        interfaceComplexityButton = interfaceHeader.GetChild(1).GetChild(0).GetComponent<Button>();
        interfaceComplexityButtonText = interfaceComplexityButton.GetComponentInChildren<Text>();
        interfaceComplexityDetails = interfaceComplexityButton.transform.GetChild(1).GetComponent<Text>();

        targetForcesButton = interfaceHeader.GetChild(3).GetChild(0).GetComponent<Button>();
        targetForcesButtonText = targetForcesButton.GetComponentInChildren<Text>();
        targetForcesDetails = targetForcesButton.transform.GetChild(1).GetComponent<Text>();

        targetMassesButton = interfaceHeader.GetChild(4).GetChild(0).GetComponent<Button>();
        targetMassesButtonText = targetMassesButton.GetComponentInChildren<Text>();
        targetMassesDetails = targetMassesButton.transform.GetChild(1).GetComponent<Text>();


        // Command listeners assignment
        pushControlStyleButton.onClick.AddListener(OnClickPushControlStyle);
        sensitivity.onValueChanged.AddListener(OnSensitivityChanged);
        smoothing.onValueChanged.AddListener(OnSmoothingChanged);
        distanceConstantSlider.onValueChanged.AddListener(OnDistanceConstantChanged);
        velocityConstantSlider.onValueChanged.AddListener(OnVelocityConstantChanged);
        forceConstantSlider.onValueChanged.AddListener(OnForceConstantChanged);
        maxRangeSlider.onValueChanged.AddListener(OnMaxRangeChanged);

        anchoredBoostButton.onClick.AddListener(OnClickAnchoredBoost);
        aNFMinimumButton.onClick.AddListener(OnClickANFMinimum);
        aNFMaximumButton.onClick.AddListener(OnClickANFMaximum);
        eWVSignageButton.onClick.AddListener(OnClickEWVSignage);
        eWVRelativityButton.onClick.AddListener(OnClickEWVRelativity);
        distanceRelationshipButton.onClick.AddListener(OnClickDistanceRelationshipButton);

        forceUnitsButton.onClick.AddListener(OnClickForceUnits);
        interfaceComplexityButton.onClick.AddListener(OnClickInterfaceComplexity);
        targetForcesButton.onClick.AddListener(OnClickTargetForces);
        targetMassesButton.onClick.AddListener(OnClickTargetMasses);

        gameplayButton.onClick.AddListener(OpenGameplay);
        physicsButton.onClick.AddListener(OpenPhysics);
        interfaceButton.onClick.AddListener(OpenInterface);
        controlSchemeButton.onClick.AddListener(OnClickControlScheme);
        rumbleButton.onClick.AddListener(OnClickRumble);
        closeButton.onClick.AddListener(OnClickClose);

        // Initial text field assignment
        controlSchemeDetails.text = "";
        controlSchemeButtonText.text = s_mk45;
        pushControlStyleButtonText.text = s_perc;
        pushControlStyleDetails.text = s_percDetails;

        forceUnitsButtonText.text = s_newt;
        forceUnitsDetails.text = s_newtDetails;
        interfaceComplexityButtonText.text = s_interfaceSums;
        interfaceComplexityDetails.text = s_interfaceSumsDetails;
        targetForcesButtonText.text = s_targetForcesEnabled;
        targetForcesDetails.text = s_targetForcesEnabledDetails;
        targetMassesButtonText.text = s_targetMassesEnabled;
        targetMassesDetails.text = s_targetMassesEnabledDetails;

        anchoredBoostButtonText.text = s_norm;
        anchoredBoostDetails.text = s_normDetails;
        aNFMinimumButtonText.text = s_aNFMinZero;
        aNFMinimumDetails.text = s_aNFMinZeroDetails;
        aNFMaximumButtonText.text = s_aNFMaxAF;
        aNFMaximumDetails.text = s_aNFMaxAFDetails;
        eWVSignageButtonText.text = s_eWVAlwaysDecreasing;
        eWVSignageDetails.text = s_eWVAlwaysDecreasingDetails;
        eWVRelativityButtonText.text = s_eWVRelative;
        eWVRelativityDetails.text = s_eWVRelativeDetails;
        distanceRelationshipButtonText.text = s_eWD;
        distanceRelationshipDetails.text = s_eWDDetails;
        forceConstantDetails.text = s_alloConstantDetails;
        maxRangeDetails.text = s_maxRDetails;

        sensitivity.value = FPVCameraLock.Sensitivity;
        smoothing.value = FPVCameraLock.Smoothing;
        distanceConstantSlider.value = PhysicsController.distanceConstant;
        velocityConstantSlider.value = PhysicsController.velocityConstant;
        forceConstantSlider.value = AllomanticIronSteel.AllomanticConstant;
        maxRangeSlider.value = AllomanticIronSteel.maxRange;

        sensitivityValueText.text = sensitivity.value.ToString();
        smoothingValueText.text = smoothing.value.ToString();
        distanceConstantValueText.text = distanceConstantSlider.value.ToString();
        velocityConstantValueText.text = velocityConstantSlider.value.ToString();
        forceConstantValueText.text = forceConstantSlider.value.ToString();
        maxRangeValueText.text = maxRangeSlider.value.ToString();

        // Now, set up the scene to start with only the Title Screen visible
        settingsHeader.gameObject.SetActive(false);
        physicsHeader.gameObject.SetActive(false);
        gameplayHeader.gameObject.SetActive(false);
        interfaceHeader.gameObject.SetActive(false);
        rumbleLabel.gameObject.SetActive(false);
        aNFMinimumLabel.gameObject.SetActive(true);
        aNFMaximumLabel.gameObject.SetActive(true);
        eWVRelativityLabel.gameObject.SetActive(false);
        eWVSignageLabel.gameObject.SetActive(false);
        distanceConstantLabel.gameObject.SetActive(true);
        velocityConstantLabel.gameObject.SetActive(false);
        CloseSettings();
    }

    public void OpenSettings() {
        gameObject.SetActive(true);
    }

    public void CloseSettings() {
        CloseGameplay();
        ClosePhysics();
        gameObject.SetActive(false);
    }

    private void OpenGameplay() {
        settingsHeader.gameObject.SetActive(false);
        gameplayHeader.gameObject.SetActive(true);
    }

    private void CloseGameplay() {
        settingsHeader.gameObject.SetActive(true);
        gameplayHeader.gameObject.SetActive(false);
    }

    private void OpenPhysics() {
        settingsHeader.gameObject.SetActive(false);
        physicsHeader.gameObject.SetActive(true);
    }

    private void ClosePhysics() {
        settingsHeader.gameObject.SetActive(true);
        physicsHeader.gameObject.SetActive(false);
    }

    private void OpenInterface() {
        settingsHeader.gameObject.SetActive(false);
        interfaceHeader.gameObject.SetActive(true);
    }

    private void CloseInterface() {
        settingsHeader.gameObject.SetActive(true);
        interfaceHeader.gameObject.SetActive(false);
    }

    public void BackSettings() {
        if (IsGameplayOpen)
            CloseGameplay();
        else if (IsPhysicsOpen)
            ClosePhysics();
        else if (IsInterfaceOpen)
            CloseInterface();
        else
            CloseSettings();
    }

    // On Button Click methods

    private void OnClickClose() {
        BackSettings();
    }

    private void OnClickControlScheme() {
        switch (GamepadController.currentControlScheme) {
            case ControlScheme.MouseKeyboard45: {
                    GamepadController.currentControlScheme = ControlScheme.MouseKeyboardQE;
                    controlSchemeButtonText.text = s_mkQE;
                    GamepadController.UsingMB45 = false;
                    break;
                }
            case ControlScheme.MouseKeyboardQE: {
                    GamepadController.currentControlScheme = ControlScheme.Gamepad;
                    controlSchemeButtonText.text = s_game;
                    controlSchemeDetails.text = s_gameDetails;
                    GamepadController.UsingGamepad = true;
                    rumbleLabel.gameObject.SetActive(true);
                    break;
                    //currentControlScheme = ControlScheme.MouseKeyboard45;
                    //controlSchemeButtonText.text = "Mouse and Keyboard (MB 4 & 5)";
                    //break;
                }
            default: {
                    GamepadController.currentControlScheme = ControlScheme.MouseKeyboard45;
                    controlSchemeButtonText.text = s_mk45;
                    controlSchemeDetails.text = "";
                    GamepadController.UsingMB45 = true;
                    GamepadController.UsingGamepad = false;
                    rumbleLabel.gameObject.SetActive(false);
                    break;
                }
        }
    }

    private void OnClickRumble() {
        if (GamepadController.UsingRumble) {
            rumbleButtonText.text = s_disabled;
            GamepadController.UsingRumble = false;
        } else {
            rumbleButtonText.text = s_enabled;
            GamepadController.UsingRumble = true;
        }
    }

    private void OnClickPushControlStyle() {
        switch (GamepadController.currentForceStyle) {
            case ForceStyle.ForceMagnitude: {
                    pushControlStyleButtonText.text = s_perc;
                    pushControlStyleDetails.text = s_percDetails;
                    GamepadController.currentForceStyle = ForceStyle.Percentage;
                    break;
                }
            default: {
                    pushControlStyleButtonText.text = s_forc;
                    pushControlStyleDetails.text = s_forcDetails;
                    GamepadController.currentForceStyle = ForceStyle.ForceMagnitude;
                    break;
                }
        }
    }

    private void OnClickForceUnits() {
        switch (displayUnits) {
            case ForceDisplayUnits.Newtons: {
                    forceUnitsButtonText.text = s_grav;
                    forceUnitsDetails.text = s_gravDetails;
                    displayUnits = ForceDisplayUnits.Gs;
                    break;
                }
            default: {
                    forceUnitsButtonText.text = s_newt;
                    forceUnitsDetails.text = s_newtDetails;
                    displayUnits = ForceDisplayUnits.Newtons;
                    break;
                }
        }
    }

    private void OnClickInterfaceComplexity() {
        switch (interfaceComplexity) {
            case InterfaceComplexity.Simple: {
                    interfaceComplexityButtonText.text = s_interfaceSums;
                    interfaceComplexityDetails.text = s_interfaceSumsDetails;
                    interfaceComplexity = InterfaceComplexity.Sums;
                    break;
                }
            default: {
                    interfaceComplexityButtonText.text = s_interfaceSimple;
                    interfaceComplexityDetails.text = s_interfaceSimpleDetails;
                    interfaceComplexity = InterfaceComplexity.Simple;
                    HUD.BurnRateMeter.InterfaceRefresh();
                    HUD.TargetOverlayController.InterfaceRefresh();
                    break;
                }
        }
    }

    private void OnClickTargetForces() {
        if (interfaceTargetForces) {
            targetForcesButtonText.text = s_targetForcesDisabled;
            targetForcesDetails.text = s_targetForcesDisabledDetails;
            interfaceTargetForces = false;
            HUD.TargetOverlayController.InterfaceRefresh();
        } else {
            targetForcesButtonText.text = s_targetForcesEnabled;
            targetForcesDetails.text = s_targetForcesEnabledDetails;
            interfaceTargetForces = true;
        }
    }

    private void OnClickTargetMasses() {
        if (interfaceTargetMasses) {
            targetMassesButtonText.text = s_targetMassesDisabled;
            targetMassesDetails.text = s_targetMassesDisabledDetails;
            interfaceTargetMasses = false;
            HUD.TargetOverlayController.InterfaceRefresh();
        } else {
            targetMassesButtonText.text = s_targetMassesEnabled;
            targetMassesDetails.text = s_targetMassesEnabledDetails;
            interfaceTargetMasses = true;
        }
    }

    private void OnSensitivityChanged(float value) {
        FPVCameraLock.Sensitivity = value;
        sensitivityValueText.text = ((int)(100 * value) / 100f).ToString();
    }

    private void OnSmoothingChanged(float value) {
        FPVCameraLock.Smoothing = value;
        smoothingValueText.text = ((int)(100 * value) / 100f).ToString();
    }

    private void OnClickAnchoredBoost() {
        switch (PhysicsController.anchorBoostMode) {
            case AnchorBoostMode.AllomanticNormalForce: {
                    velocityConstantLabel.gameObject.SetActive(true);
                    aNFMaximumLabel.gameObject.SetActive(false);
                    aNFMinimumLabel.gameObject.SetActive(false);
                    eWVSignageLabel.gameObject.SetActive(true);
                    eWVRelativityLabel.gameObject.SetActive(true);

                    PhysicsController.anchorBoostMode = AnchorBoostMode.ExponentialWithVelocity;
                    anchoredBoostButtonText.text = s_expV;
                    anchoredBoostDetails.text = s_expVDetails;
                    forceConstantSlider.value *= 2;
                    break;
                }
            case AnchorBoostMode.ExponentialWithVelocity: {
                    velocityConstantLabel.gameObject.SetActive(false);
                    eWVSignageLabel.gameObject.SetActive(false);
                    eWVRelativityLabel.gameObject.SetActive(false);
                    PhysicsController.anchorBoostMode = AnchorBoostMode.None;
                    anchoredBoostButtonText.text = s_anchorDisabled;
                    anchoredBoostDetails.text = s_anchorDisabledDetails;
                    forceConstantSlider.value /= 2;
                    break;
                }
            default: {
                    aNFMaximumLabel.gameObject.SetActive(true);
                    aNFMinimumLabel.gameObject.SetActive(true);
                    PhysicsController.anchorBoostMode = AnchorBoostMode.AllomanticNormalForce;
                    anchoredBoostButtonText.text = s_norm;
                    anchoredBoostDetails.text = s_normDetails;
                    break;
                }
        }
    }

    private void OnClickANFMinimum() {
        switch (PhysicsController.normalForceMinimum) {
            case NormalForceMinimum.Zero: {
                    PhysicsController.normalForceMinimum = NormalForceMinimum.ZeroAndNegate;
                    aNFMinimumButtonText.text = s_aNFMinZeroNegate;
                    aNFMinimumDetails.text = s_aNFMinZeroNegateDetails;
                    break;
                }
            case NormalForceMinimum.ZeroAndNegate: {
                    PhysicsController.normalForceMinimum = NormalForceMinimum.Disabled;
                    aNFMinimumButtonText.text = s_aNFMinDisabled;
                    aNFMinimumDetails.text = s_aNFMinDisabledDetails;
                    break;
                }
            default: { // Disabled
                    PhysicsController.normalForceMinimum = NormalForceMinimum.Zero;
                    aNFMinimumButtonText.text = s_aNFMinZero;
                    aNFMinimumDetails.text = s_aNFMinZeroDetails;
                    break;
                }
        }
    }

    private void OnClickANFMaximum() {
        switch (PhysicsController.normalForceMaximum) {
            case NormalForceMaximum.AllomanticForce: {
                    PhysicsController.normalForceMaximum = NormalForceMaximum.Disabled;
                    aNFMaximumButtonText.text = s_aNFMaxDisabled;
                    aNFMaximumDetails.text = s_aNFMaxDisabledDetails;
                    break;
                }
            default: { // Disabled
                    PhysicsController.normalForceMaximum = NormalForceMaximum.AllomanticForce;
                    aNFMaximumButtonText.text = s_aNFMaxAF;
                    aNFMaximumDetails.text = s_aNFMaxAFDetails;
                    break;
                }
        }
    }

    private void OnClickEWVSignage() {
        switch (PhysicsController.exponentialWithVelocitySignage) {
            case ExponentialWithVelocitySignage.AllVelocityDecreasesForce: {
                    PhysicsController.exponentialWithVelocitySignage = ExponentialWithVelocitySignage.BackwardsDecreasesAndForwardsIncreasesForce;
                    eWVSignageButtonText.text = s_eWVChangesWithSign;
                    eWVSignageDetails.text = s_eWVChangesWithSignDetails;
                    break;
                }
            case ExponentialWithVelocitySignage.BackwardsDecreasesAndForwardsIncreasesForce: {
                    PhysicsController.exponentialWithVelocitySignage = ExponentialWithVelocitySignage.OnlyBackwardsDecreasesForce;
                    eWVSignageButtonText.text = s_eWVOnlyBackwardsDecreases;
                    eWVSignageDetails.text = s_eWVOnlyBackwardsDecreasesDetails;
                    break;
                }
            default: { // Disabled
                    PhysicsController.exponentialWithVelocitySignage = ExponentialWithVelocitySignage.AllVelocityDecreasesForce;
                    eWVSignageButtonText.text = s_eWVAlwaysDecreasing;
                    eWVSignageDetails.text = s_eWVAlwaysDecreasingDetails;
                    break;
                }
        }
    }

    private void OnClickEWVRelativity() {
        switch (PhysicsController.exponentialWithVelocityRelativity) {
            case ExponentialWithVelocityRelativity.Absolute: {
                    PhysicsController.exponentialWithVelocityRelativity = ExponentialWithVelocityRelativity.Relative;
                    eWVRelativityButtonText.text = s_eWVRelative;
                    eWVRelativityDetails.text = s_eWVRelativeDetails;
                    break;
                }
            default: { // Relative
                    PhysicsController.exponentialWithVelocityRelativity = ExponentialWithVelocityRelativity.Absolute;
                    eWVRelativityButtonText.text = s_eWVAbsolute;
                    eWVRelativityDetails.text = s_eWVAbsoluteDetails;
                    break;
                }
        }
    }

    private void OnVelocityConstantChanged(float value) {
        PhysicsController.velocityConstant = ((int)value);
        velocityConstantValueText.text = ((int)value).ToString();
    }

    private void OnClickDistanceRelationshipButton() {
        switch (PhysicsController.distanceRelationshipMode) {
            case ForceDistanceRelationship.InverseSquareLaw: {
                    distanceRelationshipButtonText.text = s_linear;
                    distanceRelationshipDetails.text = s_linearDetails;
                    PhysicsController.distanceRelationshipMode = ForceDistanceRelationship.Linear;
                    forceConstantSlider.value /= 40f / 12f;
                    break;
                }
            case ForceDistanceRelationship.Linear: {
                    distanceConstantLabel.gameObject.SetActive(true);
                    distanceRelationshipButtonText.text = s_eWD;
                    distanceRelationshipDetails.text = s_eWDDetails;
                    PhysicsController.distanceRelationshipMode = ForceDistanceRelationship.Exponential;
                    break;
                }
            case ForceDistanceRelationship.Exponential: {
                    distanceConstantLabel.gameObject.SetActive(false);
                    distanceRelationshipButtonText.text = s_inverse;
                    distanceRelationshipDetails.text = s_inverseDetails;
                    PhysicsController.distanceRelationshipMode = ForceDistanceRelationship.InverseSquareLaw;
                    forceConstantSlider.value *= 40f / 12f;
                    break;
                }
        }
    }

    private void OnDistanceConstantChanged(float value) {
        PhysicsController.distanceConstant = ((int)value);
        distanceConstantValueText.text = ((int)value).ToString();
    }

    private void OnForceConstantChanged(float value) {
        AllomanticIronSteel.AllomanticConstant = ((int)value);
        forceConstantValueText.text = ((int)value).ToString();
    }

    private void OnMaxRangeChanged(float value) {
        AllomanticIronSteel.maxRange = ((int)value);
        maxRangeValueText.text = ((int)value).ToString();
    }
}
