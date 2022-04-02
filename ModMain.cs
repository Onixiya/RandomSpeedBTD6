using Assets.Scripts.Utils;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using MelonLoader;
using System;
[assembly: MelonInfo(typeof(RandomSpeed.RandomSpeed),"Random Speed","1.0.0","Silentstorm")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace RandomSpeed{
    public class RandomSpeed:BloonsTD6Mod{
        private static MelonLogger.Instance mllog=new MelonLogger.Instance("Random Speed");
        public static ModSettingInt MinSpeed=new ModSettingInt(-100);
        public static ModSettingInt MaxSpeed=new ModSettingInt(100);
        public static ModSettingInt TimeLimit=new ModSettingInt(15);
        public static float Timer=0;
        public static ModSettingBool Debug=new ModSettingBool(false);
        public static void Log(object thingtolog,string type="msg"){
            switch(type){
                case"msg":
                    mllog.Msg(thingtolog);
                    break;
                case"warn":
                    mllog.Warning(thingtolog);
                    break;
                 case"error":
                    mllog.Error(thingtolog);
                    break;
            }
        }
        public override void OnUpdate(){
            Timer+=UnityEngine.Time.fixedDeltaTime;
            if(Timer>int.Parse(TimeLimit.GetValue().ToString())){
                if(TimeManager.fastForwardActive){
                    int Speed=new Random().Next(int.Parse(MinSpeed.GetValue().ToString()),int.Parse(MaxSpeed.GetValue().ToString()));
                    if((bool)Debug.GetValue()){
                        Log("Timer: "+Timer+", TimeLimit: "+int.Parse(TimeLimit.GetValue().ToString())+", fixedDeltaTime: "+UnityEngine.Time.fixedDeltaTime+", maxSimStep: "+TimeManager.maxSimulationStepsPerUpdate);
                        Log("Speed:"+Speed+", MinSpeed: "+MinSpeed.GetValue()+", MaxSpeed: "+MaxSpeed.GetValue()+", netscale: "+TimeManager.networkScale+", nonetscale: "+TimeManager.timeScaleWithoutNetwork);
                        Log("=====================================================");
                    }
                    TimeManager.networkScale=Speed;
                    TimeManager.timeScaleWithoutNetwork=Speed;
                    TimeManager.maxSimulationStepsPerUpdate=false?1:Speed*2;
                    Timer=0;
                }else{
                    TimeManager.networkScale=1;
                    TimeManager.timeScaleWithoutNetwork=1;
                    TimeManager.maxSimulationStepsPerUpdate=1;
                }
            }
        }
    }
}