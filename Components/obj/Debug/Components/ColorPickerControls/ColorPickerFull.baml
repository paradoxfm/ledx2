   M S B A M L   `   `   `  ����E  AComponents, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:LEDX.Components.ColorPickerLEDX.Components.ColorPicker  5      i9clr-namespace:LEDX.Components.ColorPicker.ColorModels.RGB+LEDX.Components.ColorPicker.ColorModels.RGB  5      i9clr-namespace:LEDX.Components.ColorPicker.ColorModels.HSB+LEDX.Components.ColorPicker.ColorModels.HSB  5	      i9clr-namespace:LEDX.Components.ColorPicker.ColorModels.Lab+LEDX.Components.ColorPicker.ColorModels.Lab  5
      k:clr-namespace:LEDX.Components.ColorPicker.ColorModels.CMYK,LEDX.Components.ColorPicker.ColorModels.CMYK  5      9    3LEDX.Components.ColorPickerControls.ColorPickerFull   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8my)clr-namespace:LEDX.Components.ColorPicker     5      Imy19clr-namespace:LEDX.Components.ColorPicker.ColorModels.RGB     5      Imy39clr-namespace:LEDX.Components.ColorPicker.ColorModels.HSB     5	      Imy49clr-namespace:LEDX.Components.ColorPicker.ColorModels.Lab     5
      Jmy5:clr-namespace:LEDX.Components.ColorPicker.ColorModels.CMYK     5      $	��275��5      $	��528��6,   .+�5      /   )LEDX.Components.ColorPicker.ColorSelector  -   5           Name$  colorSelector��5   M   $	��270��6   $
��Left=�6!   $��0,0,0,0q�6<   $	��Top=�5      $	��314��6#      SelectionRingMode$ BlackAndWhite=�6/   <   6LEDX.Components.ColorPicker.ColorModels.RGB.RgbDisplay  5      -   5          Name$ 
rgbDisplay��5   F   $��84��6
   $
��Left=�6   $��319,157,0,0q�61   $	��Top=�5   
   $��90��6"      Color 5      ��  �� ElementName$ colorSelector�� �� Path$ Color� �� Mode$ TwoWay=�   NormalComponent 5   
   �� $ colorSelector��$ NormalComponent�$ TwoWay=�5   "   <   6LEDX.Components.ColorPicker.ColorModels.HSB.HsbDisplay  5      -   5          Name$ hsbDisplay1��5   9   $��90��5   J   $	��Top=�6V   $
��Left=�5   
   $��
318,58,0,0q�6%   $��94��6N   	   Color	 5      �� $ colorSelector��$ Color�
   NormalComponent
 5   
   �� $ colorSelector��$ NormalComponent�$ TwoWay=�<   6LEDX.Components.ColorPicker.ColorModels.Lab.LabDisplay  5      -   5          Name$ labDisplay1��5   9   $��90��5   J   $
��Left=�5   
   $��
427,58,0,0q�6%   $	��Top=�5   
   $��90��6"      Color 5      �� $ Color�$ colorSelector��   NormalComponent 5   
   �� $ colorSelector��$ NormalComponent�$ TwoWay=�5   "   ,   &LEDX.Components.ColorPicker.HexDisplay  5      -   5          Name$ hexDisplay1��5   B   $��25��5      $
��Left=�6   $��336,239,0,0q�69   $	��Top=�5   W   $��73��6o      Color 6
   �� $ Color�$ colorSelector��6o   >   8LEDX.Components.ColorPicker.ColorModels.CMYK.CMYKDisplay  5      -   5          Name$ cMYKDisplay2��5   <   $	��112��5   K   $
��Left=�5      $��444,155,0,0q�6'   $	��Top=�5       $��73��6#      DisplayMode$ PercentNoDecimal=�6.      Color 5      �� $ Color�$ colorSelector��5    .   ,   &LEDX.Components.ColorPicker.NewCurrent  5!      -   5          Name$ 
newCurrent��5!   ?   $
��Left=�6   $��	320,8,0,0q�6,   $	��Top=�6S   $��40��5"   
   $	��177��6      Orientation$ 
Horizontal=�6"      NewColor 5#   
   �� $ colorSelector��$ Color�5$      