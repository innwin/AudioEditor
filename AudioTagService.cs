using TagLib;

namespace AudioEditor;

public static class AudioTagService
{
    private static readonly string[] AudioExtensionList = ["aa", "aax", "aac", "aiff", "ape", "dsf", "flac", "m4a", 
        "m4b", "m4p", "mp3", "mpc", "mpp", "ogg", "oga", "wav", "wma", "wv", "webm"];
    public static void Set(AudioConfig config)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        Console.WriteLine($"当前路径 : {currentDirectory}");
        
        var dirPath = string.IsNullOrWhiteSpace(config.Finder) ? currentDirectory : $"{currentDirectory}/{config.Finder}";
        var dir = new DirectoryInfo(dirPath);
        var files = dir.GetFiles();
        var filesCount = files.Length;
        var setCount = 0;
        for (var i = 0; i < files.Length; i++)
        {
            var file = files[i];
            var fileExtension = file.Extension.Replace(".", "");
            if (!AudioExtensionList.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
            {
                continue;
            }
            
            Console.WriteLine($"修改文件Tag ({i + 1} / {filesCount}) : {file.Name}");
            var audioFile = TagLib.File.Create(file.FullName);
            // 标题
            if (config.IsClearTitle == true)
            {
                audioFile.Tag.Title = null;
            } 
            else if (config.IsTitleSameWithFileName == true)
            {
                audioFile.Tag.Title = file.Name.Replace(file.Extension, "");
            }
            // 专辑
            if (!string.IsNullOrWhiteSpace(config.Album))
            {
                audioFile.Tag.Album = config.Album;
            }
            // 表演者
            if (config.AlbumArtists != null)
            {
                audioFile.Tag.AlbumArtists = config.AlbumArtists;
            }
            // 流派
            if (config.Genres != null)
            {
                audioFile.Tag.Genres = config.Genres;
            }
            // 图片
            if (config.Pictures is { Length: > 0 })
            {
                var pictures = new IPicture[config.Pictures.Length];
                for (var j = 0; j < config.Pictures.Length; j++)
                {
                    var picture = config.Pictures[j];
                    pictures[j] = new Picture(picture.Contains('/') ? picture : $"{currentDirectory}/{picture}");
                }
                audioFile.Tag.Pictures = pictures;
            }
            audioFile.Save();

            setCount++;
        }
        
        Console.WriteLine($"设置完成, 处理了 {setCount} 个文件");
    }
}