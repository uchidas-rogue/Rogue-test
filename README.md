# Rogue-test

hashimoto 環境構築完了

## Mac用コマンド

DockからLaunchpad→その他→ターミナルでターミナルを起動  
以下のコマンドを入力  
`/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install.sh)"`  
`brew install git`  
`brew install git-lfs`  
`brew cask install google-chrome`  
`brew cask install unity-hub`  
`brew cask install visual-studio-code`  
`brew cask install zoomus`  
`brew cask install gimp`  

Update用のコマンド  
``brew update; brew upgrade `brew list`; brew cask upgrade `brew cask list` ``  

vscodeのUpdate時の注意  
インテリセンスが効かなくなったらmonoを更新  
->それでもダメなら、C#の拡張のバージョンをアップデート前に戻す

## 「Unity hub」のインストール

 「Unity2019.4.6f1」のインストール  
 ※VisualStudio,Andoroid,ios,日本語のコンポーネントを追加してインストール  

## 「VScode 1.45.0 64bit」のインストール

* Mac用 設定追記  
  1. spotlightのキーバインドを変更  
  システム環境設定→spotlight→キーバインド  
  「spotlightの検索を表示」のチェックを外す  
  1. インテリセンスのサジェストトリガーの設定  
  `command+k -> command+s`  
  以下の文字で検索  
  `editor.action.triggerSuggest`  
  キーバインドを「command+space」に変更  

* 公式サイトからインストーラをダウンロードしてインストール  
 「mono-6.8.0.123-x64」のインストール（Macで必須）  
   入れないとコード補完がきかない  
 「.NET Framework 4.7.1」のインストール（Windowsで必須）  
   入れないとコード補完がきかない  
 「.NET core 3.1 SDK」のインストール（任意）  
   入れると余計なエラー表示が消える  

* 必要なVScodeの拡張機能  
 C# powered by OmniSharp  
 C# Extensions  
 C# FixFormat  
 C# XML Documentation Comments  
 Debugger for Unity  
 Git History  
 Japanese Language Pack for VS Code  
  以下は任意  
 GitLens  
 markdownlint  
 Unity Code Snippets  
 vscode-icons  
 zenkaku  

## 「Git for Windows -2.26.2 64bit」のインストール

 1. ユーザー名とメールアドレスの登録
「git bash」を起動して  
  `git config --global user.name "ユーザー名"`  
  `git config --global user.email "メールアドレス"`  

## 「git-lfs-windows-v2.11.0.exe」のインストール

1. Git Large File Storage の公式サイトよりインストール
 ※画像や音声などのサイズの大きいファイル用

## 「GIMP 2.10.18」のインストール

1. photoshopの代わり、最新版でもok

## 「Unity2019.3.13f1」の環境設定

1. VScodeをデフォルトエディターに設定  
 「Edit」タブを開き「Preferences…」をクリック  
 「External Tools」の「External Script Editor」を「VisualStudioCode」に変更  
 ~~※「Generate .csproj files for:」のパラメータ全部チェックをつけて「Regenerate project files」~~  
1. ~~プラットホームをAndoroidに変更~~  
 ~~「File」タブを開き「Build Setting」をクリック~~  
 ~~「Android」を選択して「Switch Platform」をクリック~~  
1. 「CRIware」の導入  
 「ADX2 LE」を公式サイトよりダウンロード  
 C:\criにダウンロードしたファイルを展開  
 Unityにて「Asset」タブを開き「Import Package」=>「Custom Package…」をクリック  
 C:\cri\unity\pluginにあるパッケージを導入（チェックマークはデフォルト）  
1. TextMesh Pro + M＋Fontsの導入  
 TextMesh Pro -> PackageManagerからインストール  
 M+Fonts -> otfファイルは https://mplus-fonts.osdn.jp/about.html よりダウンロード  
 下記のリンク先を参考に導入、※ttfはmplus-2c-regular.ttf※Atlas Resolutionは4096  
 https://hi-network.sakura.ne.jp/wp/2020/04/15/post-1407/  
 
