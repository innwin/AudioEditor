// See https://aka.ms/new-console-template for more information

using System.Text.Encodings.Web;
using System.Text.Json;
using AudioEditor;

var jsonOptions = new JsonSerializerOptions
{
    // DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,    // 忽略null值的属性
    PropertyNameCaseInsensitive = true,    //忽略大小写
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,    // 驼峰式
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,    // 序列化中文时的编码问题
};

// {"isClearTitle":true,"isTitleSameWithFileName":true,"finder":null,"album":"中华五千年","albumArtists":["香港电台文教组"],"genres":["粤语评书"],"pictures":["中华五千年.jpg"]}
Console.WriteLine($"参数 : {string.Join(", ", args)}");
if (args.Length == 0)
{
    Console.WriteLine("未设置参数, 不执行任务");
}
else
{
    var audioConfig = JsonSerializer.Deserialize<AudioConfig>(args[0], jsonOptions);
    if (audioConfig != null)
    {
        Console.WriteLine($"识别到参数 : {JsonSerializer.Serialize(audioConfig, jsonOptions)}");
        AudioTagService.Set(audioConfig);
    }
}

// var config = new AudioConfig(true, true, null, "中华五千年", ["香港电台文教组"], ["粤语评书"], ["中华五千年.jpg"]);
// Console.WriteLine(JsonSerializer.Serialize(config, jsonOptions));
//
// var currentDirectory = Directory.GetCurrentDirectory();
// Console.WriteLine($"当前路径 : {currentDirectory}");
//
// var readText = File.ReadAllText($"{currentDirectory}/中华五千年/000中华五千年目录.txt");
// var audioFileNameList = readText.Replace(".mp3", "").Split("  ").Select(p => p.Trim()).ToArray();
// Console.WriteLine($"音频文件名, 个数 : {string.Join(", ", audioFileNameList.Length)}, 前3 : {string.Join(Environment.NewLine, audioFileNameList)}");
//
// var dirPath = $"{currentDirectory}/中华五千年";
// var dir = new DirectoryInfo(dirPath);
// foreach (var file in dir.GetFiles())
// {
//     Console.WriteLine($"文件名 : {file.Name.Replace(file.Extension, "")}, 大小 : {file.Length}");
//     if (file.Extension == ".mp3")
//     {
//         var audioFileName = audioFileNameList.FirstOrDefault(p => p.Contains(file.Name.Replace(file.Extension, "")));
//         if (audioFileName != null)
//         {
//             File.Move($"{dirPath}/{file.Name}", $"{dirPath}/{audioFileName}{file.Extension}");
//         }
//     }
// }
//
// var dirPath = $"{currentDirectory}/中华五千年";
// var dir = new DirectoryInfo(dirPath);
// var files = dir.GetFiles();
// var filesCount = files.Length;
// for (var i = 0; i < files.Length; i++)
// {
//     var file = files[i];
//     if (file.Extension == ".mp3")
//     {
//         Console.WriteLine($"修改文件Tag ({i + 1} / {filesCount}) : {file.Name}");
//         var audioFile = TagLib.File.Create(file.FullName);
//         // 标题
//         audioFile.Tag.Title = file.Name.Replace(file.Extension, "");
//         // 专辑
//         audioFile.Tag.Album = "中华五千年";
//         // 表演者
//         audioFile.Tag.AlbumArtists = ["香港电台文教组"];
//         // 流派
//         // audioFile.Tag.Genres = ["粤语评书"];
//         audioFile.Save();
//     }
// }