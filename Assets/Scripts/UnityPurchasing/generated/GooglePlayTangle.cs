// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("H9qohidOwM/kh3h662wi/A9ZEy/MXfnx8UMlhKcjcZjJTYAePgcF1L0PjK+9gIuEpwvFC3qAjIyMiI2Oy/nXy5R2DFj6n8Zc9oFGDy0LRCtFNZJA0cw5J56skPfy9szcSfk9N0ByCVPgkemJQkM6n00h6HI8eXbwt8QuwFy9zs8XPVZQ+eTpSoqwTbwPjIKNvQ+Mh48PjIyNGwnxXG67kCjA8ErvzTvolOOOkiPjNynEsOW5kZJ61hK0dVCwG5gGMGWWUoZOZZkeOoPTry8UjbomAwclK8M/DXlsgvaGHNxkfn03hwYEFC/5RGW3ONtORb5LlvgsLYPYn8k7dV793fjutyxpZomHN54Hn094vE3p8bI0tYqI2rz8biaB7TBYZo+OjI2M");
        private static int[] order = new int[] { 3,12,3,7,13,6,7,12,13,13,11,12,13,13,14 };
        private static int key = 141;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
