namespace r6random
{
    
    public class OperatorInfo
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public bool Enabled { get; set; } = true;
        public string ImagePath => $@"Res\{Role}s\{Name}.png";
        public string IconPath => $@"Res\{Role}s\{Name}_ICON.png";
    }
}
