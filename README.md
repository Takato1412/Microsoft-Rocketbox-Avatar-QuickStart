# Microsoft-Rocketbox-Avatar-QuickStart

## Introdution
Microsoft が 115 体のアバタからなるデータベース [Microsoft Rocketbox Avatar Library](https://github.com/microsoft/Microsoft-Rocketbox) を公開しています（2022/11//18）。
すでに多くのVR研究者がこれを採用しており、本データセットを活用することは研究同士の比較や再現性の向上に役立つと考えられます。

本クイックスタートでは、主にアバタへの Embodiment を目的として、**Meta Quest シリーズ と Microsoft Rocketbox Avatar を組み合わせて使用する方法** を紹介します。
現在までに実装できている項目は、以下の通りです。

- Touch コントローラを使って、Microsoft Rocketbox Avatar を三点トラッキングで動かす。
- Quest の Hand Tracking 機能を使って、Microsoft Rocketbox Avatar を三点 + ハンドトラッキングで動かす。

詳細な実装方法や拡張のヒントについては、Zenn に書籍としてまとめましたので、そちらを参照してください。
https://zenn.dev/tmizuho/books/8c447297fd05b4

## Demo
https://user-images.githubusercontent.com/40706341/202663909-6ec48c7d-7cd8-4836-8456-8e8a71f94dc8.mp4

## Quick Start
### 1. Unity プロジェクトの作成と Meta Quest 用のセットアップ
以下のサイトを参考に、Unity のセットアップをしてください。
https://framesynthesis.jp/tech/unity/oculusquest/

> [!WARNING]
> Oculus Integration が deprecated (非推奨) になり、代わりに Meta XR Core SDK などのアセットを使うようになりました。2023/12/7 時点では、(OVRCameraRigなどのGUIDが変わっていなかったので)、どちらを使っても問題なく動作しました。しかし、今後のことを考えると、Meta XR Core SDK に移行する方が賢明だと思います。

### 2. Asset Store (Package Manager) から外部アセットをインポート
Meta XR Core SDK (Oculus Integration) 以外に、以下のアセットを使用するため、インポートしてください。

- [Final IK](https://assetstore.unity.com/packages/tools/animation/final-ik-14290): 有料アセット。頭、右手、左手の三点のトラッキング情報から、アバタの全身動作を計算します。必須ではありませんが、無料のIKよりも精度が良いように思ったため、採用しています。
- [SAColliderBuilder](https://assetstore.unity.com/packages/tools/sacolliderbuilder-15058): 無料アセット。アバタの形に沿うようにコライダを生成し、外界とのインタラクションを可能にします。

### 3. 手製の unitypackage をインポート
リリースノートから MSRAQuickStart.unitypackage をダウンロードし、インポートしてください。

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

任意の Scene に `Assets/MizuhoLab/Prefabs/OVRCameraRig_Plus/OVRCameraRig+MSRAwithHandTracking.prefab` を Drag and Drop すると、**Quest Hand Tracking を用いた三点+ハンドトラッキング**を試すことが出来ます。

### 5. サンプルシーン
`Assets/Scenes/` には、以下のサンプルシーンが含まれています。参考にしてください。

| Name | Description |
| :--- | :--- |
| MSRA_FinalIK | Oculus Touch コントローラを使って、Microsoft Rocketbox Avatar を三点トラッキングで動かす。また、コントローラのボタン入力に応じて、指も動かす。|
| MSRA_HandTracking | Meta Quest Hand Tracking を使って、Microsoft Rocketbox Avatar を三点 + ハンドトラッキングで動かす。|
| MSRA_SelfLipSync | (MSRA_HandTracking +) OVRLipSync を使って、マイク入力に応じて Microsoft Rocketbox Avatar の口を動かす。|
| MSRA_NPC | (MSRA_SelfLipSync +) アニメーションファイルを使って、Microsoft Rocketbox Avatar を NPC として動かす。また、OVRLipSync を使って、音声ファイルに応じて NPC の口を動かす。|
| MSRA_ARKit | (MSRA_SelfLipSync +) Unity Live Capture を使って、Microsoft Rocketbox Avatar の表情をフェイストラッキングで動かす。|

### 6. 拡張のヒント

Zenn に詳細な実装方法などをまとめた書籍を公開しましたので、ぜひ参照してください。
https://zenn.dev/tmizuho/books/8c447297fd05b4

## Summary
**Microsoft Rocketbox Avatar に Meta Quest を用いて Embodiment するクイックスタート** を作成しました。様々なVR研究において、本システムが実験システム構築の手間軽減に貢献できたら嬉しいです。

自分用に開発したという側面が強いので、必要に応じて機能を拡張していく予定です。(e.g., Meta Quest Pro を使ったフェイストラキング)

また、同じく Embodiment を目的としたクイックスタートプログラムとして [QuickVR](https://github.com/eventlab-projects/com.quickvr.quickbase) が先日公開されました。こちらはより汎用性が高く（Humanoidモデル全てに適用可）、機能も豊富で（異なる身長のモデルへの Embodiment など）、実験フローの管理を支援するプログラムなども提供されているようです。2022/11/18 閲覧時点ではまだいくつかのバグを抱えているようですが、動向を見守りたいと思います。
