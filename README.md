# Rogue-test

## 「Unity hub」のインストール

 「Unity2019.3.13f1」のインストール
 ※VisualStudioはインストールしない（VScodeで代用）
 ※Documentation,Andoroid,日本語のコンポーネントを追加してインストール

## 「VScode 1.45.0 64bit」のインストール

* 公式サイトからインストーラをダウンロードしてインストール  
 「mono-6.8.0.123-x64」のインストール（必須かどうか不明）  
   入れないとコード補完がきかない？  
 「.NET Framework 4.7.1」のインストール（絶対必須）  
   入れないとコード補完がきかない  
 「.NET core 3.1 SDK」のインストール（任意）  
   入れると余計なエラー表示が消える  

* 必要なVScodeの拡張機能
 C# powered by OmniSharp  
 C# Extensions  
 C# FixFormat  
 Debugger for Unity  
 Mono Debug <=必須かどうか不明  
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
 ※「Generate .csproj files for:」のパラメータ全部チェックをつけて「Regenerate project files」  
2. プラットホームをAndoroidに変更  
 「File」タブを開き「Build Setting」をクリック  
 「Android」を選択して「Switch Platform」をクリック  
3. 「CRIware」の導入  
 「ADX2 LE」を公式サイトよりダウンロード  
 C:\criにダウンロードしたファイルを展開  
 Unityにて「Asset」タブを開き「Import Package」=>「Custom Package…」をクリック  
 C:\cri\unity\pluginにあるパッケージを導入（チェックマークはデフォルト）  
