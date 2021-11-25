namespace HackedDesign
{
    public interface ILevelGenerator
    {
        Level GenerateLevel(LevelGenTemplate template);
        Level GenerateLevel(LevelGenTemplate template, int enemies);
        Level GenerateLevel(LevelGenTemplate template, int length, int height, int width, int enemies);
    }
}