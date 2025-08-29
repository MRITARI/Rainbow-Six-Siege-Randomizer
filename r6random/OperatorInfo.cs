namespace r6random
{
    public class OperatorInfo
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public bool Enabled { get; set; } = true;
        public string ImagePath => $@"{Role}s\{Name}.png";
        public string IconPath => $@"{Role}s\{Name}_ICON.png";
    }
}
