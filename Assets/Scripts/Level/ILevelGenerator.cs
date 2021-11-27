namespace HackedDesign
{
    public interface ILevelGenerator
    {
        Level GenerateLevel(LevelGenTemplate template, int levelCount);
        Level GenerateLevel(LevelGenTemplate template, int levelCount, int enemies);
        Level GenerateLevel(LevelGenTemplate template, int levelCount, int length, int height, int width, int enemies);
    }
}