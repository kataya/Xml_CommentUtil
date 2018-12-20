
# XmlCommentUtility

## 概要

ArcMap でのデフォルト ロケータの設定（DefaultLocators.xml）を、有効化/無効化を切り替えするツールです。

通常は DefaultLocators.xml の有効化/無効化には、XMLファイルをテキストエディタで編集する必要がありますが、
本ツールを利用するとGUIで切り替えすることが可能です。

コンパイル済みのアプリケーションは[こちら](https://github.com/kataya/Xml_CommentUtil/releases) にあります。

切り替えは <portal_locators> で定義されたXML要素と、<locator_ref> で定義されたXML要素に対応しています。

![デフォルト ロケータ設定変更ツール](https://github.com/kataya/Xml_CommentUtil/img/XmlCommentUtility_main.png)

## 仕様

無効化するとArcMap の [ジオコーディング] ツールバー、[検索] ダイアログ ボックスからの利用ができなくなります。

ArcMap でのデフォルト ロケータの仕様は、以下のHelpをご参照ください。

http://desktop.arcgis.com/ja/arcmap/latest/manage-data/geocoding/setting-default-locators-in-arcmap.htm

## 制限

本ツールは システム デフォルト ロケータの設定を行うことを前提にしているため、実行するには管理者モードで起動していただく必要があります。
