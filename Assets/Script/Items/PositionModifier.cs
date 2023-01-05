
    using System;
    using TMPro;
    using UnityEngine;
[Serializable]
    public class PositionModifier : ItemProperty
    {
       
        private Character _user;

        public void Initialize(Character user)
        {
            _user = user;
        }
        
        public override void Use(Character target, Field field, Item item)
        {
            var user = _user.transform.position;
            var tar = target.transform.position;
            Vector2 dest;
             dest.x = tar.x - user.x;
             dest.y = tar.y - user.y;
             Vector2 d = tar += (Vector3)dest;
             target.Move(d);
            // target.transform.position +=(Vector3) dest;
        }
        

        public override TextMeshProUGUI Text(TextMeshProUGUI text)
        {
            return text;
        }

        public override float StatusDamageFill()
        {
            return 0;
        }
    }
