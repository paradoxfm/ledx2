   M S B A M L   `   `   `  ����M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  5       
Components   LEDX.Dialogs.ErrorDialog   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      3Fl$clr-namespace:Fluent;assembly=Fluent     5      $	��350��5      $	��500��6     � WindowStyle$  SingleBorderWindow=�6+    � 
ResizeMode$ NoResize=�6L      Fluent.RibbonWindow   GlassBorderThickness$ 5,25,5,5q�6b    � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5   (    � Title$' !Внутренняя ошибка��6L   .��5      � $��0,30,0,0q�6    � RowDefinitions 5      �� 5	      $
��Auto �6   �� 5
      �� 5      $
��Auto �6   .+�5      �� $��0��6
   $?��9Вознило внутреннее исключение:��6   �� 5      -   5        $��svLog��5      $��1��6   $
��Auto=�6+   $��Disabled=�6N   $	��5,0q�6w   .��5      �� -   5        $��tbErr��5       �� 
IsReadOnly .  �� AcceptsReturn . 	 �� TextWrapping$
	 Wrap=�6A   5      �� 5      $��2��6   
 �� Orientation$
 
Horizontal=�6   $��Right=�65   .+�5      �� -   5        $0��*Копировать и отправить��5      $	��150��6-   $	��5,5q�69   6F   �� 5      -   5        $��Закрыть��5      $��80��6   $	��5,5q�6)   66   5      5      5      