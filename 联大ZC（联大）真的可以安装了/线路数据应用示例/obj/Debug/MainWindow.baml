   M S B A M L   `   `   `  ����S  O线路数据应用示例, Version=0.1.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ZCZC  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       ZC.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:ZC     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$  ZC��5	   	   $	��600��6   $	��800��6!    � WindowStartupLocation$ CenterScreen=�5
   	    � 	Resources 5         myString��%'   &	        ��5   
   	hello��6-   5      .��5      � .+�5   
   �� -   5        $��
MainScroll��5   _   $
��Auto=�6   $
��Auto=�6<   .��5      �� -   5        $��
MainCanvas��5      
���   �$
��4000��6:   $
��1080��6G   6�   5      o� 5   
   -   5         o�Name$
 menu��5      $��23.333��6   $	��Top=�6-   .@�5      m� $��菜单��6   .@�5      m� -   5        $��退出��5      6'   5      m� 5      $��编辑��6   .@�5      m� -   5        $��网络配置��5      6C   5      m� 5      $��日志��6   .@�5      m� -   5        $��OpenLogs��5      $��打开日志窗口��6+    m� IsCheckable . 6g   m� 5      -   5        $��SaveLogs��5      $��关闭时保存日志��6+    . 6h   5      m� 5      $��关于��6   .@�5      m� -   5        $��关于软件��5      6?   5      5       5!      5#      