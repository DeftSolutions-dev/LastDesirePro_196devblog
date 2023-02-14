using static LastDesirePro.Menu.CFG.AimBotConfig;
using LastDesirePro.Menu.CFG;
using UnityEngine;
using LastDesirePro.Main.Visuals;
using System.IO;
using System.Collections.Generic;
using static LastDesirePro.DrawMenu.Prefab;
using System;

namespace LastDesirePro.Menu.MenuTab {
  internal class AimBot {
    public static Vector2 _AimBot, _Silent, _pSilent, _Hit1, _Hit, _Friend, _Friend1;
    public static float f = 0;
    public static float he = 100;
    public static int tab = 0;
    public static string[] tb = new string[] {
      "AimBot",
      "Silent Aim",
      "pSilent",
      "HitChange",
      "Friend List"
    };
    private Dictionary < string, string > boneNames = new Dictionary < string, string > () {
      {
        "body",
        "тело"
      }, {
        "chest",
        "грудь"
      }, {
        "groin",
        "в хуй"
      }, {
        "head",
        "голову"
      }, {
        "hip",
        "таз"
      }, {
        "jaw",
        "челюсть"
      }, {
        "left arm",
        "в руку"
      }, {
        "left eye",
        "в глаз"
      }, {
        "left foot",
        "в ногу"
      }, {
        "left forearm",
        "в предплечье"
      }, {
        "left hand",
        "в руку"
      }, {
        "left knee",
        "в колено"
      }, {
        "left ring finge",
        "в палец"
      }, {
        "left shoulder",
        "в плечо"
      }, {
        "left thumb",
        "в палец"
      }, {
        "left toe",
        "в палец"
      }, {
        "left wrist",
        "в запястье"
      }, {
        "lower spine",
        "в хребет"
      }, {
        "neck",
        "шею"
      }, {
        "pelvis",
        "таз"
      }, {
        "right arm",
        "в руку"
      }, {
        "right eye",
        "в глаз"
      }, {
        "right foot",
        "в ступню"
      }, {
        "right forearm",
        "в предплечье"
      }, {
        "right hand",
        "в руку"
      }, {
        "right knee",
        "в колено"
      }, {
        "right ring finge",
        "в палец"
      }, {
        "right shoulder",
        "в плечо"
      }, {
        "right thumb",
        "в палец"
      }, {
        "right toe",
        "в палец"
      }, {
        "right wrist",
        "в запястье"
      }, {
        "stomach",
        "живот"
      }
    };
    public static bool key, key1, key2;
    public static void DoAimBot() {
      tab = Tab(23, 60, 133, 100, tb, tab, 150);
      switch (tab) {
      case 0: {
        GUIContent[] boxInt = {};
        ScrollViewMenu(new Rect(15, 85, 375, 415), ref _AimBot, () => {
          GUILayout.BeginVertical(GUILayout.Width(180));
          GUILayout.Space(5 f);
          Toggle("Aim", ref _aim);
          if (_aim) {
            Toggle("Aim Fov", ref _aimFov, 17, _colorFov, true, 89);
            Slider(0 f, 380 f, ref _aimfov, 200, $ "{string.Format(" {
                0
              }
              ", (int)_aimfov)}.");
            Toggle("Prediction", ref _aimPred);
            GUILayout.Space(5 f);
            using(new GUILayout.HorizontalScope()) {
              GUIContent[] target = {
                new GUIContent("Head"),
                new GUIContent("Neck"),
                new GUIContent("Chest"),
                new GUIContent("Body")
              };
              if (Button("◄", 25)) {
                _aimBodyList--;
                if (_aimBodyList == -1)
                  _aimBodyList = 0;
              }
              GUILayout.Label(target[(int) _aimBodyList], _TextStyle1);
              if (Button("►", 25)) {
                _aimBodyList++;
                if (_aimBodyList == 4)
                  _aimBodyList = 3;
              }
            }
            _aimkey = Bind(_aimkey, "Aim Key: ", ref key);
          }
          GUILayout.Space(5 f);
          GUILayout.EndVertical();
        });
        break;
      }
      case 1: {
        ScrollViewMenu(new Rect(15, 85, 750, 415), ref _Silent, () => {
          GUILayout.BeginVertical(GUILayout.Width(180));
          GUILayout.Space(5 f);
          Toggle("Magic Bullet Player's [Detect Normal Server]", ref _silentPlayerPredict, 17);
          Toggle("Silent Wall [Setting Bone to HitChange(Hit Players Bone)] [Detect Normal Server]", ref _silentPlayerPredictWall, 17);
          if (_silentPlayerPredict || _silentPlayerPredictWall) {
            Toggle("Fov", ref _silentPlayerPredictFov, 17, _silentPlayerPredictFovColor, true, 96);
            Slider(0 f, 380 f, ref _silent, 200, $ "{string.Format(" {
                0
              }
              ", (int)_silent)}.");
          }
          Toggle("Magic Bullet HeliCopter", ref _silentHeliPredict, 17);
          GUILayout.EndVertical();
        });
        break;
      }
      case 2: {
        ScrollViewMenu(new Rect(15, 85, 370, 415), ref _pSilent, () => {
          GUILayout.BeginVertical(GUILayout.Width(180));
          GUILayout.Space(5 f);
          Toggle("pSilent", ref _pAim);
          if (_pAim) {
            Toggle("Double shot", ref _pAimDub);
            Toggle("Target", ref _pAimTarget, 17, _pAimTargetColor, true, 95);
            Toggle("pSilent Fov", ref _pAimFov, 17, _colorpAimFov, true, 90);
            Slider(0 f, 380 f, ref _pAimfov, 200, $ "{string.Format(" {
                0
              }
              ", (int)_pAimfov)}.");
            GUILayout.Space(5 f);
            using(new GUILayout.HorizontalScope()) {
              GUIContent[] target = {
                new GUIContent("Head"),
                new GUIContent("Chest"),
                new GUIContent("Body")
              };
              if (Button("◄", 25)) {
                _pAimBodyList--;
                if (_pAimBodyList == -1)
                  _pAimBodyList = 0;
              }
              GUILayout.Label(target[(int) _pAimBodyList], _TextStyle1);
              if (Button("►", 25)) {
                _pAimBodyList++;
                if (_pAimBodyList == 3)
                  _pAimBodyList = 2;
              }
            }
            GUILayout.Space(5 f);
            _pAimkey = Bind(_pAimkey, "pSilent Key: ", ref key1);
            Toggle("maxDamage [HvH - Normal Server DETECTED]", ref _maxDamage);
          }
          GUILayout.EndVertical();
        });
        ScrollViewMenu(new Rect(395, 85, 370, 415), ref _pSilent, () => {
          GUILayout.BeginVertical(GUILayout.Width(180));
          GUILayout.Space(5 f);
          Toggle("pSilent Heli", ref _pAimHeli);
          if (_pAimHeli)
            _pAimHelikey = Bind(_pAimHelikey, "pSilent Heli Key: ", ref key2);
          GUILayout.EndVertical();
        });
        break;
      }
      case 3: {
        ScrollViewMenu(new Rect(15, 85, 370, 415), ref _Hit, () => {
          GUILayout.BeginVertical(GUILayout.Width(180));
          GUILayout.Space(5 f);
          Toggle("HitBox Helicopter", ref _hitBoxHelicopter);
          if (_hitBoxHelicopter)
            Slider(0 f, 40 f, ref _hitBoxHelicopterRadius, 200, $ "{string.Format(" {
                0: 0. #
              }
              ", (float)_hitBoxHelicopterRadius)} m.");
          Toggle("HitBox Players", ref _hitBoxPlayer);
          if (_hitBoxPlayer) {
            Toggle("Hit Effect Glass", ref _hitBoxPlayerGlass);
            GUILayout.Space(5 f);
            using(new GUILayout.HorizontalScope()) {
              GUIContent[] target = {
                new GUIContent("Head"),
                new GUIContent("Chest"),
                new GUIContent("Body")
              };
              if (Button("◄", 25)) {
                _hitBoxPlayerList--;
                if (_hitBoxPlayerList == -1)
                  _hitBoxPlayerList = 0;
              }
              GUILayout.Label(target[(int) _hitBoxPlayerList], _TextStyle1);
              if (Button("►", 25)) {
                _hitBoxPlayerList++;
                if (_hitBoxPlayerList == 3)
                  _hitBoxPlayerList = 2;
              }
            }
            Slider(0 f, 3 f, ref _hitBoxPlayerRadius, 200, $ "{string.Format(" {
                0: 0. #
              }
              ", (float)_hitBoxPlayerRadius)} m.");
          }
          GUILayout.EndVertical();
        });
        ScrollViewMenu(new Rect(395, 85, 370, 415), ref _Hit1, () => {
          GUILayout.BeginVertical(GUILayout.Width(180));
          GUILayout.Space(5 f);

          Toggle("Hit Helicopter Only Rotor", ref _hitRotor);
          Toggle("Hit Players Bone", ref _hitPlayer);
          if (_hitPlayer) {
            GUILayout.Space(5 f);
            using(new GUILayout.HorizontalScope()) {
              GUIContent[] target = {
                new GUIContent("Head"),
                new GUIContent("Chest"),
                new GUIContent("Body")
              };
              if (Button("◄", 25)) {
                _hitPlayerList--;
                if (_hitPlayerList == -1)
                  _hitPlayerList = 0;
              }
              GUILayout.Label(target[(int) _hitPlayerList], _TextStyle1);
              if (Button("►", 25)) {
                _hitPlayerList++;
                if (_hitPlayerList == 3)
                  _hitPlayerList = 2;
              }
            }
          }
          GUILayout.EndVertical();
        });
        break;
      }
      case 4: {
        ScrollViewMenu(new Rect(15, 85, 370, 415), ref _Friend, () => {
          GUILayout.BeginVertical(GUILayout.Width(230));
          SearchString = TextField(SearchString, "Player Search: ", 175);
          if (LocalPlayer.Entity != null) {
            for (int i = 0; i < BasePlayer.VisiblePlayerList.Count; i++) {
              BasePlayer player = BasePlayer.VisiblePlayerList[i];
              if (player == LocalPlayer.Entity || player == null || (SearchString != "" && player.displayName.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) == -1))
                continue;
              bool Friend = Main.AimBot.AimBot.IsFriend(player);
              bool Selected = player == Main.AimBot.AimBot.meme_target;
              string color = Friend ? "<color=green>" : "";
              GUILayout.Space(4);
              if (Button((Selected ? "<b>" : "") + color + $ "{player.displayName}" + (Friend ? "</color>" : "") + (Selected ? "</b>" : ""), 275))
                Main.AimBot.AimBot.meme_target = player;
            }
          }
          GUILayout.EndVertical();
        });
        ScrollViewMenu(new Rect(395, 85, 250, 415), ref _Friend1, () => {
          GUILayout.BeginVertical(GUILayout.Width(200));
          GUILayout.Space(10 f);
          if (Main.AimBot.AimBot.IsFriend(Main.AimBot.AimBot.meme_target)) {
            if (Button("Remove Friend ", 205))
              Main.AimBot.AimBot.RemoveFriend(Main.AimBot.AimBot.meme_target);
          } else {
            if (Button("Add Friend ", 205))
              Main.AimBot.AimBot.AddFriend(Main.AimBot.AimBot.meme_target);
          }
          Toggle("No Damage Friend", ref _noDamageFriend);
          if (Main.AimBot.AimBot.meme_target != null && LocalPlayer.Entity != null) {
            GUILayout.Space(10 f);
            GUILayout.Label("SteamID:", _TextStyle1);
            GUILayout.TextField(Main.AimBot.AimBot.meme_target.userID.ToString());
            GUILayout.Space(2);
            GUILayout.Label("How much HP: ", _TextStyle1);
            GUILayout.Label((Main.AimBot.AimBot.meme_target != null ? string.Format("{0}HP", (int) Main.AimBot.AimBot.meme_target.health) : "[---]"), _TextStyle1);
            GUILayout.Label("What's in the Hands:", _TextStyle1);
            GUILayout.Label(((Main.AimBot.AimBot.meme_target.GetHeldEntity() != null) ? Main.AimBot.AimBot.meme_target.GetHeldEntity().GetItem().info.displayName.english : ("Nothing")), _TextStyle1);
          }
          GUILayout.EndVertical();
        });
        break;
      }
      }
    }
    public static string SearchString = "";
  }
}
