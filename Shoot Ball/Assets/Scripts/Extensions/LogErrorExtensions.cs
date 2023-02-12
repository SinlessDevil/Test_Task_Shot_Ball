namespace Extensions{
    public static class LogErrorExtensions{
        public static void LogError<T>(T componnet){
            if (componnet == null)
                throw new System.NullReferenceException($"The {componnet.ToString()} Component is not assigned in the inspector!");
        }
    }
}