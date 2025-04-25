using UnityEngine;

namespace CrystalOfTime.Systems.Command
{
    public class CheckTagCondition : ConditionBase
    {
        [SerializeField] private string _tagForCheck;
        public override bool Check(object data = null)
        {
            if (data is string)
            {
                if ((string)data == _tagForCheck)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}