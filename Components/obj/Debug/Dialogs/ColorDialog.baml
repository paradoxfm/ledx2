   M S B A M L   `   `   `  ����M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  5   
   E AComponents, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:LEDX.Components.ColorPickerControls#LEDX.Components.ColorPickerControls 5   
      LEDX.Dialogs.ColorDialog   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   
   3Fl$clr-namespace:Fluent;assembly=Fluent     5   
   EPickers1clr-namespace:LEDX.Components.ColorPickerControls     5   
     � Title$  Color Picker��5   
   $	��370��6   $	��532��6,    � WindowStyle$ SingleBorderWindow=�5   
    � 
ResizeMode$ NoResize=�6+      Fluent.RibbonWindow   GlassBorderThickness$ 5,25,5,5q�6A    � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5   
   .��5	      � $��0,30,0,0q�6    � RowDefinitions 5
      �� 5      $��30 �6   �� 5      $	��270 �6   �� 5      $��30 �6   .+�5      �� $��0��6   $��Center=�6    �� Orientation$ 
Horizontal=�69   .+�5      d� -   5        $��	brStColor��5      $��Right=�6#   $��30��6?   .��5      �� $ 
Horizontal=�6   .+�5      �� $��0,5,5,0q�6L    d� 	IsChecked 6   �� 	 �� Path$	 	IsChecked�
 �� ElementName$
 	brStColor��6L   �� 5      -   5        $��	rectStCol��5      $��37��6"   $��26��6-   5      5      �� 5      -   5        $��
btSwpColor��5      $��Center=�6   $��Center=�6;   $��25��6V   $��25��6a   $
��10,0q�6m   6{   d� 5      -   5        $��
btEndColor��5      $
��Left=�6$   $��30��6?   .��5      �� $ 
Horizontal=�6   .+�5      �� -   5        $��	rectEnCol��5      $��37��6"   $��26��6-   �� 5      $��5,5,0,0q�6M    6   �� $	 	IsChecked�$
 
btEndColor��6M   5      5      5      9  3LEDX.Components.ColorPickerControls.ColorPickerFull  5      -   5          Name$ colorPickerFull��5   D   9  3LEDX.Components.ColorPickerControls.ColorPickerFull   SelectedColorChanged$* $colorPickerFull_SelectedColorChanged��5   9   $��1��5      $
��Left=�6)   $	��Top=�5      $��4,0,0,0q�6(   69   �� 5!      $ 
Horizontal=�6   $��2��6(   $
��Left=�65   $��Center=�6P   $��	189,0,0,0q�6k   .+�5"      �� -   5        $
��btOk��5"   R   $��OK��6   $��23��6   $
��Left=�6&   $��0,0,4,0q�6A   $	��Top=�6^   $��75��6v   6�   �� 5#      -   5        $��btCancel��5#   b   $��Отмена��6   $��1��6   $��23��6*   $
��Left=�66   $��8,0,0,0q�6Q   $	��Top=�6r   $��75��6�   6�   5$      �� 5%      -	   5        $��	cbClrSend��5%   N   $!��Живой просмотр��6   $��16��6&   $��Right=�62   6N   5&      5'      