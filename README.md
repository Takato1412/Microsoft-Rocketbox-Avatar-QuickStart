# Microsoft-Rocketbox-Avatar-QuickStart

## Introdution
Microsoft が 115 体のアバタからなるデータベース [Microsoft Rocketbox Avatar Library](https://github.com/microsoft/Microsoft-Rocketbox) を公開しています（2022/11//18）。
すでに多くのVR研究者がこれを採用しており、本データセットを活用することは研究同士の比較や再現可能性の向上に役立つと考えられます。

本クイックスタートでは、**主にアバタへの Embodiment を目的として、Meta Quest 2 と Microsoft Rocketbox Avatar を組み合わせて使用する方法**を紹介します。
現在までに実装できている項目は、以下の通りです。

- Touch コントローラを使って、Microsoft Rocketbox Avatar を三点トラッキングで動かす。
- Quest の Hand Tracking 機能を使って、Microsoft Rocketbox Avatar を三点 + ハンドトラッキングで動かす。
- その他、VR内での鏡やボタンなどのUIも、副次的に使えるようになる。

https://user-images.githubusercontent.com/40706341/202663909-6ec48c7d-7cd8-4836-8456-8e8a71f94dc8.mp4

## Quick Start
### 1. Unity プロジェクトの作成と Meta Quest 2 用のセットアップ
以下のサイトを参考に、Unity のセットアップをしてください。
https://framesynthesis.jp/tech/unity/oculusquest/

> [!WARNING]
> Oculus Integration が deprecated (非推奨) になり、代わりに Meta XR Core SDK などのアセットを使うようになりました。2023/12/7 時点では、(OVRCameraRigなどのGUIDが変わっていなかったので)、どちらを使っても問題なく動作しました。しかし、今後のことを考えると、Meta XR Core SDK に移行する方が賢明だと思います。

### 2. Asset Store (Package Manager) から外部アセットをインポート
Meta XR Core SDK (Oculus Integration) 以外に、以下のアセットを使用するため、インポートしてください。

- [Final IK](https://assetstore.unity.com/packages/tools/animation/final-ik-14290): 有料アセット。頭、右手、左手の三点のトラッキング情報から、アバタの全身動作を計算します。必須ではありませんが、無料のIKよりも精度が良いように思ったため、採用しています。
- [SAColliderBuilder](https://assetstore.unity.com/packages/tools/sacolliderbuilder-15058): 無料アセット。アバタの形に沿うようにコライダを生成し、外界とのインタラクションを可能にします。

### 3. 手製の unitypackage をインポート
リリースノートから [MSRAQuickStart.unitypackage](https://github.com/Takato1412/Microsoft-Rocketbox-Avatar-QuickStart/releases/tag/v2022.0805) をダウンロードし、インポートしてください。
内容は以下の通りです。
| Directory | Description |
| :--- | :--- |
|`MicrosoftRocketboxAvatars/` | Microsoft Rocketbox Avatar Library から男性アバタ (Male_Adult_08) と女性アバタ (Female_Adult_01) の3Dモデルが一体ずつ入っています。|
| `MicrosoftRocketboxMovebox/` | [MoveBox-for-Microsoft-Rocketbox](https://github.com/microsoft/MoveBox-for-Microsoft-Rocketbox) を改変した、IKとハンドトラッキング用のスクリプトが入っています。|
| `MirrorReflection/` | VR用の鏡。Unityフォーラムの[スレッド](https://forum.unity.com/threads/mirror-reflections-in-vr.416728/)を元に作成しました。|
| `MizuhoLab/` | その他、自作のスクリプトやプレファブが入っています。|
| `Scenes/` | サンプルシーンが入っています。|

### 4. 実行
任意の Scene に `Assets/MizuhoLab/Prefabs/OVRCameraRig_Plus/OVRCameraRig+MSRA.prefab` を Drag and Drop すると、**Touch コントローラを用いた三点トラッキング**を試すことが出来ます。
`Assets/Scenes/MicrosoftRocketboxAvatar_FinalIK.scene` がサンプルシーンです。

任意の Scene に `Assets/MizuhoLab/Prefabs/OVRCameraRig_Plus/OVRCameraRig+MSRAwithHandTracking.prefab` を Drag and Drop すると、**Quest Hand Tracking を用いた三点+ハンドトラッキング**を試すことが出来ます。
`Assets/Scenes/MicrosoftRocketboxAvatar_HandTracking.scene` がサンプルシーンです。

サンプルシーンには、以下の機能を持ったボタンも配置していますので、参考にしてください。
- アバタを切り替える
- アバタの身長をユーザの身長に合わせて拡大・縮小する

### 5. 拡張のヒント
詳細な実装の過程などは以下のページに示していますので、参考にしてください。
https://tmizuho.notion.site/How-to-use-Microsoft-Rocketbox-Avatar-bd1eb73e0d4f417bbbedb7b3ba66a173

## Summary
**Microsoft Rocketbox Avatar に Meta Quest 2 を用いて Embodiment するシステム**を作成しました。様々なVR研究において、本クイックスタートが実験システム構築の手間軽減に貢献できたらと思います。

将来的には、Meta Quest Pro に対応し、以下の項目も実装したいと考えています。
- リップシンク、フェイストラッキング
- フルボディトラッキング

また、同じく Embodiment を目的としたクイックスタートプログラムとして [QuickVR](https://github.com/eventlab-projects/com.quickvr.quickbase) が先日公開されました。こちらはより汎用性が高く（Humanoidモデル全てに適用可）、機能も豊富で（異なる身長のモデルへの Embodiment など）、実験フローの管理を支援するプログラムなども提供されているようです。2022/11/18閲覧時点ではまだいくつかのバグを抱えているようですが、動向を見守りたいと思います。
