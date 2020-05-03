# MemoBird_GuGu
这是一个运行在 Windows 上的「咕咕机」打印软件。
> 什么是「咕咕机」？[点我跳转到「咕咕机」官网](http://www.memobird.cn/)

这是个人项目，非官方项目，但官方也提供了相关的开发文档，详见[「咕咕机」开放平台](https://open.memobird.cn/)。

考虑到打印机是一个队列操作，同时为了让程序更“简单”，所以程序没有实现异步操作。

## 隐私保护
所有打印内容都由软件直接处理，并向「咕咕机」官方服务发起 HTTP 请求，未通过任何第三者转发处理，请放心使用。

您也可自行克隆项目进行构建。

本软件中的“历史”及“设备”功能，文件保存在以下目录中：
```
C:\Users\[您的用户名]\AppData\Local\MemoBird_GuGu\
```
**注意：请不要随意更改此目录下的文件，您的操作可能会导致软件无法正常运行。如因误操作导致软件无法正常运行，您可删除此目录，并重启软件，但这也意味着您将丢失历史记录。**

## 软件界面
![软件界面](https://github.com/PublicPwd/MemoBird_GuGu/raw/master/MemoBird_GuGu/images/%E6%96%87%E6%9C%AC.png "软件界面")

## 下载
在这里[下载](https://github.com/PublicPwd/MemoBird_GuGu/releases)

## 项目结构
```
MemoBird_GuGu
├── Classes                         // 数据模型及全局变量
├── OpenLibrary                     // 三方库
│   ├── QRCoder                     // 二维码生成
│   └── ggApi                       // 官方的图像二值化
├── Pages                           // 子页面，与软件中的导航栏对应
│   ├── Page_Device.xaml            // 设备
│   ├── Page_History.xaml           // 历史
│   ├── Page_Image.xaml             // 图片
│   ├── Page_QRCode.xaml            // 二维码
│   ├── Page_Text.xaml              // 文本
│   ├── Page_TextAndImage.xaml      // 拼接
├── Properties                      // 项目属性
├── Resources                       // 项目资源
├── Utils                           // 工具
│   ├── CheckUpdate.cs              // 检查更新
│   ├── FileX.cs                    // 文件操作
│   ├── HttpHelper.cs               // http 请求
│   ├── Parsing.cs                  // 文本解析
│   └── WebApi                      // 咕咕机 API
├── Windows                         // 窗口
│   ├── Window_About.xaml           // 关于
│   ├── Window_AddText.xaml         // 添加文本
│   ├── Window_DeviceDetails.xaml   // 设备详情
│   ├── Window_HistoryDetails.xaml  // 打印历史详情
│   ├── Window_Main.xaml            // 主窗口
│   ├── Window_Tip.xaml             // 轻提示
└── images                          // 软件截屏
```

## 如何使用
### 添加设备
首先需要在设备页中添加设备。

点击右侧【添加】按钮，输入「咕咕机」相关信息，点击【确认】。
```
设备名称：任意，相当于设备的昵称。
设备编号：「咕咕机」的设备编号。
```
> 如何获取设备编号？双击「咕咕机」按键，在打印的纸条中查找。

![设备](https://github.com/PublicPwd/MemoBird_GuGu/raw/master/MemoBird_GuGu/images/%E8%AE%BE%E5%A4%87.png "设备")

### 文本
在左侧输入框中输入需要打印的内容，点击右侧【发送】开始打印。

![文本](https://github.com/PublicPwd/MemoBird_GuGu/raw/master/MemoBird_GuGu/images/%E6%96%87%E6%9C%AC.png "文本")

### 图片
点击右侧【图片】按钮选择需要打印的图片，左侧可预览打印效果，点击右侧【发送】开始打印。

![图片](https://github.com/PublicPwd/MemoBird_GuGu/raw/master/MemoBird_GuGu/images/%E5%9B%BE%E7%89%87.png "图片")

### 拼接
此页面可将文字和图片在同一张纸条中打印。

点击右侧【文本】或【图片】添加需要打印的内容，点击右侧【上移】、【下移】、【移除】对打印内容进行调整，点击右侧【发送】开始打印。

![拼接](https://github.com/PublicPwd/MemoBird_GuGu/raw/master/MemoBird_GuGu/images/%E6%8B%BC%E6%8E%A5.png "拼接")

### 二维码
在左侧下拉框中选择需要打印的二维码类型，点击右侧【发送】按钮开始打印。

![二维码](https://github.com/PublicPwd/MemoBird_GuGu/raw/master/MemoBird_GuGu/images/%E4%BA%8C%E7%BB%B4%E7%A0%81.png "二维码")

### 历史
选择开始日期及结束日期，点击右侧【查找】，查找该时间段内打印的内容。

双击打印记录或点击右侧【详情】查看打印内容。

点击右侧【重新打印】，打印选中的历史记录。

![历史](https://github.com/PublicPwd/MemoBird_GuGu/raw/master/MemoBird_GuGu/images/%E5%8E%86%E5%8F%B2.png "历史")

## 三方库（感谢）
gugu-.net: 实现图像二值化。
```
https://github.com/memobird/gugu-.net
```

QRCoder: 实现二维码生成。
```
QRCoder https://github.com/codebude/QRCoder
```