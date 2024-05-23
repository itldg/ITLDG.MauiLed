# 七段码 LED

该库继承`GraphicsView`使用`IDrawable`绘制七段码

## 效果预览

![效果预览](https://raw.githubusercontent.com/itldg/ITLDG.MauiLed/main/imgs/preview.png)

## 可用属性

| 属性名     | 类型  | 说明                                             |
| ---------- | ----- | ------------------------------------------------ |
| LightColor | Color | 点亮颜色                                         |
| DarkColor  | Color | 熄灭颜色                                         |
| Data       | byte  | 七段码数据,解析方法参照[七段码解析](#七段码解析) |
| Inversion  | bool  | 数据反转,反转后七段码数据 0 是点亮,1 是熄灭      |

## 使用示例

```xml
xmlns:itldg="clr-namespace:ITLDG.MauiLed;assembly=ITLDG.MauiLed"

....

<itldg:LedView Data="255" HeightRequest="80" WidthRequest="52" NightColor="Black" LightColor="OrangeRed" />

<itldg:LedView Data="255" HeightRequest="80" WidthRequest="52" Inversion="true" />

<itldg:LedView Data="{Binding Val}" HeightRequest="80" WidthRequest="52" LightColor="Green" Inversion="true"/>
```

## 七段码解析

默认 `1 表示点亮` `0 表示熄灭` 下图表示 Bit 位与七段码的对应关系

![七段码](https://raw.githubusercontent.com/itldg/ITLDG.MauiLed/main/imgs/led.png)

## 扩展方法

| 方法                                                       | 说明                             |
| ---------------------------------------------------------- | -------------------------------- |
| LedData.GetText(byte data, bool inversion = false)         | 获取展示字符,获取不到返回问号(?) |
| LedData.HasDecimalPoint(byte data, bool inversion = false) | 该数据是否包含小数点             |
| LedData.GetData(char str, bool inversion = false)          | 获取字符对应的七段码数据         |
