# Microsoft-Rocketbox-Avatar-QuickStart

## Introdution
Microsoft が 115 体のアバタからなるデータベース [Microsoft Rocketbox Avatar Library](https://github.com/microsoft/Microsoft-Rocketbox) を公開しています（2022/11//18）。
すでに多くのVR研究者がこれを採用しており、本データセットを活用することは研究同士の比較や再現可能性の向上に役立つと考えられます。

本クイックスタートでは、主にアバタへの Embodiment を目的として、Meta Quest 2 と Microsoft Rocketbox Avatar を組み合わせる方法を紹介します。
現在までに実装できている項目は、以下の通りです。

- Touch コントローラを使って、Microsoft Rocketbox Avatar を三点トラッキングで動かす。
- Quest の Hand Tracking 機能を使って、Microsoft Rocketbox Avatar を三点 + ハンドトラッキングで動かす。
- その他、VR内での鏡やボタンなどのUIも、副次的に使えるようになる。

将来的には、以下の項目も実装したいと考えています。

- リップシンク、フェイストラッキング
- フルボディトラッキング

## Quick Start
### 1. Unity プロジェクトの作成と Meta Quest 2 用のセットアップ
以下のサイトを参考に、Unity のセットアップをしてください。
https://framesynthesis.jp/tech/unity/oculusquest/

### 2. Asset Store (Package Manager) から外部アセットをインポート
Oculus Integration 以外に、以下のアセットを使用するため、インポートしてください。

- [Final IK](https://assetstore.unity.com/packages/tools/animation/final-ik-14290): 有料アセット。頭、右手、左手の三点のトラッキング情報から、アバタの全身動作を計算します。必須ではありませんが、無料のIKよりも精度が良いように思ったため、採用しています。
- [SAColliderBuilder](https://assetstore.unity.com/packages/tools/sacolliderbuilder-15058): 無料アセット。アバタの形に沿うようにコライダを生成し、外界とのインタラクションを可能にします。

### 3. 手製の unitypackage をインポート
リリースノートから MSRAQuickStart.unitypackage をダウンロードし、インポートしてください。
Assets/ に以下が取り込まれます。
- MicrosoftRocketboxAvatars/ : Microsoft Rocketbox Avatar Library から男性アバタ (Male_Adult_08) と女性アバタ (Female_Adult_01) の3Dモデルが一体ずつ入っています。
- MicrosoftRocketboxMovebox/ : [MoveBox-for-Microsoft-Rocketbox](https://github.com/microsoft/MoveBox-for-Microsoft-Rocketbox) を改変した、IKとハンドトラッキング用のスクリプトが入っています。
- MirrorReflection/ : VR用の鏡。Unityフォーラムの[スレッド](https://forum.unity.com/threads/mirror-reflections-in-vr.416728/)を元に作成しました。
- MizuhoLab/ : その他、自作のスクリプトやプレファブが入っています。
- Scenes/ : サンプルシーン

### 4. 実行
任意の Scene に Assets/MizuhoLab/Prefabs/OVRCameraRig_Plus/OVRCameraRig+MSRA.prefab を Drag and Drop すると、Touch コントローラを用いた三点トラッキングを試すことが出来ます。
Assets/Scenes/MicrosoftRocketboxAvatar_FinalIK.scene がサンプルシーンです。

任意の Scene に Assets/MizuhoLab/Prefabs/OVRCameraRig_Plus/OVRCameraRig+MSRAwithHandTracking.prefab を Drag and Drop すると、Quest Hand Tracking を用いた三点+ハンドトラッキングを試すことが出来ます。
Assets/Scenes/MicrosoftRocketboxAvatar_HandTracking.scene がサンプルシーンです。
