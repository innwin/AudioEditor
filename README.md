# 音频标签编辑

## 设置环境变量

`~/.bash-profile`

```shell
export AUDIO_EDITOR_HOME="/Users/x/Projects/bin/AudioEditor"
PATH="$PATH:$AUDIO_EDITOR_HOME"
```

## 运行

```shell
AudioEditor '{"isClearTitle":true,"isTitleSameWithFileName":true,"finder":null,"album":"中华五千年","albumArtists":["香港电台文教组"],"genres":["粤语评书"],"pictures":["中华五千年.jpg"]}'
```