   M S B A M L   `   `   `  ����E  ALE2, Version=1.0.5702.30957, Culture=neutral, PublicKeyToken=null1clr-namespace:LEDX.ConvertersLEDX.Converters  5      -clr-namespace:LEDX.CommandsLEDX.Commands  5      1clr-namespace:LEDX.ComponentsLEDX.Components  5          LEDX.Components.UiTrack   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -locclr-namespace:LEDX.Converters     5      )cclr-namespace:LEDX.Commands     5      ,myclr-namespace:LEDX.Components     5      -   5        $
��Auto��5      $��40��6   $��0,0,0,1q�6    $��Stretch=�6P   ��61   �� *Frames+  � 	Resources  5	      !   LEDX.Converters.InverseBool   conInvBl&   LEDX.Converters.SelectController  selConverter��%:   &	        &	        5
      6     5      6   5      ��5      �� 5       �� 
StartPoint$
 .5,1&�6    �� EndPoint$
 .5,0&�6*   .��5      �  � Offset$ 0Q�6    � Color 6   �� *
IsSelected+ �� 	Converter# [ � 5      $ 1Q�6   $
 Gray��6   5       � ContextMenu 5      �� 5      ��5      -  'System.Windows.Data.CompositeCollection  5      m� 5      $A��;Сохранить контроллер как шаблон��6    m� Command$ c:Cmd.AddSample��69    m� CommandParameter$
 True��6S   	 m� Icon$	 /Images/colors_16.png��5      �� 5      m� 5      $��Удалить��6   $ c:Cmd.DelContr��6!   $	 /Images/delete_16.png��6:   5      5      ��5"      �� 5#      .R�5$         LEDX.Components.UiFrame  
 � DataContext
 6   �� 5%      ��5(      �� 5)      .R�5*      �� -   5        $
��Left=�5*       �� Orientation$ 
Horizontal=�6+   5+      5,      5.      