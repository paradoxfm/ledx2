   M S B A M L   `   `   `  ����  
Components.    (LEDX.Components.ColorPicker.AlphaDisplay   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5       5      �� 5      $ SliderResources.xaml?�6   �� 5      $ CheckerBoardBrush.xaml?�6   %    5
      .��5      � $��48��6	    � RowDefinitions 5      �� 5      �� 5      $��24 �6   .+�5      �� -   5        $��txtAlpha��5   D   $��23��6   $
��Left=�6   $��0,0,0,0q�63   $	��Top=�6T   $��28��6l   $��1��5      ��6   ��  �� ElementName$ sAlpha�� �� Path$ Value� �� StringFormat$ {0:N0}���� 5      $��24��6D   $��2,4,2,4q�6O   $
��Left=�6`   ��6   ��    CheckerBoard �� Source# [  6`   �� 5      
�������$��2q�6   $��0��62   $��0,4,0,4q�6B    J� SnapsToDevicePixels . $��28��6n   $
��Left=�5      $��Stretch=�6&   ._�5      �� -   5        $��imgTransparency��5      $
��Fill=�6"   5      5      �� 5      -   5        $��sAlpha��5   8   $
��Left=�6   $��18,0,0,0q�6&   $��Stretch=�6F   	 �� Orientation$	 Vertical=�6b   $��28��5        SliderStyle#���  6   
 � Maximum$	
 100Q�6<    � Minimum$ 0Q�6J    �� IsSnapToTickEnabled .  � Value$	 100Q�5      6   5      5       