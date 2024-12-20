# CubicBody
## 概要
棒を用いて体の動きを制限した状態で、幾何学形態の3Dモデルに合わせた様々な動きをすることで、人間から離れた形状のアバターに没入感を持たせるVR体験

## 開発期間
2022年5月 - 2023年12月

## 使用技術
Unity 2021.3.6f1  
SteamVR  
HTC Vive  
Vive Stereo Rendering ToolKit  
└─ 鏡を使って体験者にアバターの全体像を見せるのに使用

## 公開先
(映像) https://youtu.be/aKnm7gYWtdE  
(展示) Siggraph Asia 2023 Sydney

## ディレクトリ構成
```bash
.
├── .vscode
├── .gitignore
├── .gitattributes
├── Packages
├── Assets
│   ├── _Main
│   │   ├── 0912 // 幾何学形態の3Dモデル
│   │   ├── 1007 // 幾何学形態の3Dモデル
│   │   ├── Images 
│   │   ├── Materials 
│   │   ├── Prefabs // アバターのプレファブ
│   │   ├── Scripts // 各アバターに対応する動作、マネージャー
│   │   └── Scenes 
│   │       └──CubicBodyScene.unity // 本番のシーン
.   .
.   .
.   .
```
