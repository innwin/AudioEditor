namespace AudioEditor;

public record AudioConfig(
    bool? IsClearTitle, 
    bool? IsTitleSameWithFileName, 
    string? Finder, 
    string? Album, 
    string[]? AlbumArtists, 
    string[]? Genres,
    string[]? Pictures);