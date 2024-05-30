using System.Collections.Generic;
using System.Text;

namespace Project.Utils{
    public static class StringBuilderPool{
        private static List<StringBuilder> _stringBuilders = new List<StringBuilder>();
        public static StringBuilder GetStringBuilder(){
            if(_stringBuilders.Count > 0){
                var sb = _stringBuilders[_stringBuilders.Count - 1];
                _stringBuilders.RemoveAt(_stringBuilders.Count - 1);
                sb.Clear();
                return sb;
            }else{
                return new StringBuilder();
            }
        }

        public static void ReleaseStringBuilder(StringBuilder sb){
            if(sb == null) return;

            sb.Clear();
            _stringBuilders.Add(sb);
        }
    }
}