using System.Windows;
using System.Windows.Input;
using LEDX.Utils;

namespace LEDX.Commands {

	public class Cmd {

		#region System commands
		#region Exit
		private static BaseCommand _exitCmd;
		public static ICommand ExitCommand {
			get { return _exitCmd ?? (_exitCmd = new BaseCommand(x => Application.Current.Shutdown())); }
		}
		#endregion

		#region New
		private static BaseCommand _newCmd;
		public static ICommand NewCommand {
			get { return _newCmd ?? (_newCmd = new BaseCommand(x => FileOperations.NewFile())); }
		}
		#endregion

		#region Open
		private static BaseCommand _openCmd;
		public static ICommand OpenCommand {
			get { return _openCmd ?? (_openCmd = new BaseCommand(x => FileOperations.OpenFile())); }
		}
		#endregion

		#region Save
		private static BaseCommand _saveCmd;
		public static ICommand SaveCommand {
			get { return _saveCmd ?? (_saveCmd = new BaseCommand(x => FileOperations.SaveFile(), y => EditUtil.Changed(), true)); }
		}
		#endregion

		#region SaveAs
		private static BaseCommand _saveasCmd;
		public static ICommand SaveAsCommand {
			get { return _saveasCmd ?? (_saveasCmd = new BaseCommand(x => FileOperations.SaveAsFile())); }
		}
		#endregion

		#region Undo
		private static BaseCommand _undoCmd;
		public static ICommand UndoCommand {
			get { return _undoCmd ?? (_undoCmd = new BaseCommand(x => EditUtil.Undo(), y => EditUtil.CanUndo(), true)); }
		}
		#endregion

		#region Redo
		private static BaseCommand _redoCmd;
		public static ICommand RedoCommand {
			get { return _redoCmd ?? (_redoCmd = new BaseCommand(x => EditUtil.Redo(), y => EditUtil.CanRedo(), true)); }
		}
		#endregion

		#region SetTheme
		private static BaseCommand _setTheme;
		public static ICommand SetTheme {
			get { return _setTheme ?? (_setTheme = new BaseCommand(App.SetTheme)); }
		}
		#endregion

		#region Help
		private static BaseCommand _helpCmd;
		public static ICommand HelpCommand {
			get { return _helpCmd ?? (_helpCmd = new BaseCommand(x => EditUtil.Help())); }
		}
		#endregion
		
		#region Error cmd
		private static BaseCommand _errCmd;
		public static ICommand ErrorCommand {
			get { return _errCmd ?? (_errCmd = new BaseCommand(x => EditUtil.Error())); }
		}
		#endregion
		#endregion

		#region Буфер обмена
		#region Copy Frames
		private static BaseCommand _copyFrame;
		public static ICommand CopyFrame {
			get { return _copyFrame ?? (_copyFrame = new BaseCommand(x => ClipboardUtil.CopyFrame())); }
		}
		#endregion

		#region Copy Color
		private static BaseCommand _copyColor;
		public static ICommand CopyColor {
			get { return _copyColor ?? (_copyColor = new BaseCommand(x => ClipboardUtil.CopyColor())); }
		}
		#endregion

		#region Cut Frame
		private static BaseCommand _cutFrame;
		public static ICommand CutFrame {
			get { return _cutFrame ?? (_cutFrame = new BaseCommand(x => ClipboardUtil.CutFrame())); }
		}
		#endregion

		#region Paste object
		private static BaseCommand _pasteObject;
		public static ICommand PasteObject {
			get {
				return _pasteObject ??
						(_pasteObject = new BaseCommand(x => ClipboardUtil.PasteObject(), y => ClipboardUtil.CanPaste(false), true));
			}
		}
		#endregion

		#region Paste object
		private static BaseCommand _pasteBefore;
		public static ICommand PasteBefore {
			get {
				return _pasteBefore ??
						(_pasteBefore = new BaseCommand(x => ClipboardUtil.PasteBefore(), y => ClipboardUtil.CanPaste(true), true));
			}
		}
		#endregion
		#endregion

		#region Цвета
		#region Solid Color Установить сплошной цвет
		private static BaseCommand _setSolidColor;
		public static ICommand SetSolidColor {
			get { return _setSolidColor ?? (_setSolidColor = new BaseCommand(x => ColorUtil.SetSolidColor())); }
		}
		#endregion

		#region SetLRColor взять цвета соседей
		private static BaseCommand _setLrColor;
		public static ICommand SetLrColor {
			get { return _setLrColor ?? (_setLrColor = new BaseCommand(x => ColorUtil.SetLrColor())); }
		}
		#endregion

		#region SetColorEnd установить начальный цвет
		private static BaseCommand _setColorBeg;
		public static ICommand SetColorBeg {
			get { return _setColorBeg ?? (_setColorBeg = new BaseCommand(x => ColorUtil.SetColor(false))); }
		}
		#endregion

		#region SetColorEnd установить конечный цвет
		private static BaseCommand _setColorEnd;
		public static ICommand SetColorEnd {
			get { return _setColorEnd ?? (_setColorEnd = new BaseCommand(x => ColorUtil.SetColor(true))); }
		}
		#endregion
		#endregion

		#region Редактор
		#region DelFrame Удалить фрейм
		private static BaseCommand _delFrame;
		public static ICommand DelFrame {
			get { return _delFrame ?? (_delFrame = new BaseCommand(x => EditUtil.DelFrame())); }
		}
		#endregion

		#region DelController Удалить контроллер
		private static BaseCommand _delContr;
		public static ICommand DelContr {
			get {
				return _delContr ??
						(_delContr = new BaseCommand(x => EditUtil.DelController(false), y => EditUtil.CanDelContr(), true));
			}
		}
		#endregion

		#region SetScaleDefault установить масштаб по умолчанию
		private static BaseCommand _defScale;
		public static ICommand DefScale {
			get { return _defScale ?? (_defScale = new BaseCommand(x => EditUtil.SetScaleDefault())); }
		}
		#endregion

		#region AddController Добавить контроллер
		private static BaseCommand _addContr;
		public static ICommand AddContr {
			get { return _addContr ?? (_addContr = new BaseCommand(x => EditUtil.AddController(null))); }
		}
		#endregion

		#region AddSample Экспорт в сэмпл
		private static BaseCommand _addSample;
		public static ICommand AddSample {
			get { return _addSample ?? (_addSample = new BaseCommand(EditUtil.AddSample)); }
		}
		#endregion

		#region DelSample Экспорт в сэмпл
		private static BaseCommand _delSample;
		public static ICommand DelSample {
			get { return _delSample ?? (_delSample = new BaseCommand(x => EditUtil.DelSample())); }
		}
		#endregion

		#region AddFrame добавить фрейм
		private static BaseCommand _addFrame;
		public static ICommand AddFrame {
			get { return _addFrame ?? (_addFrame = new BaseCommand(EditUtil.AddFrame)); }
		}
		#endregion

		#region InsertSample вставить сэмпл в контроллер
		private static BaseCommand _insertSample;
		public static ICommand InsertSample {
			get { return _insertSample ?? (_insertSample = new BaseCommand(EditUtil.InsertSample)); }
		}
		#endregion

		#endregion

		#region Проигрывание

		#region открыть фон анимации
		private static BaseCommand _openBackground;
		public static ICommand OpenBackground {
			get { return _openBackground ?? (_openBackground = new BaseCommand(x => PlayerUtil.OpenBackgroundImage())); }
		}
		#endregion

		#region открыть источник света
		private static BaseCommand _openLigth;
		public static ICommand OpenLigth {
			get { return _openLigth ?? (_openLigth = new BaseCommand(x => PlayerUtil.OpenLigth())); }
		}
		#endregion
		
		#region преобразовать в карту освещенности
		private static BaseCommand _convertLigth;
		public static ICommand ConvertLigth {
			get { return _convertLigth ?? (_convertLigth = new BaseCommand(x => PlayerUtil.ConvertLigth())); }
		}
		#endregion

		#region режим редактирования
		private static BaseCommand _editMode;
		public static ICommand EditMode {
			get { return _editMode ?? (_editMode = new BaseCommand(PlayerUtil.EditMode)); }
		}
		#endregion

		#region экспорт в png
		private static BaseCommand _exportToPng;
		public static ICommand ExportToPng {
			get { return _exportToPng ?? (_exportToPng = new BaseCommand(x => PlayerUtil.SaveCanvas())); }
		}
		#endregion ExportToAVI
		
		#region экспорт в avi
		private static BaseCommand _exportToAvi;
		public static ICommand ExportToAvi {
			get { return _exportToAvi ?? (_exportToAvi = new BaseCommand(x => PlayerUtil.SaveVideo())); }
		}
		#endregion


		#region вставить текст
		private static BaseCommand _insertLabel;
		public static ICommand InsertLabel {
			get { return _insertLabel ?? (_insertLabel = new BaseCommand(x => PlayerUtil.InsertLabel())); }
		}
		#endregion

		#region опустить слой
		private static BaseCommand _layerDown;
		public static ICommand LayerDown {
			get { return _layerDown ?? (_layerDown = new BaseCommand(x => PlayerUtil.LayerDown())); }
		}
		#endregion

		#region поднять слой
		private static BaseCommand _layerUp;
		public static ICommand LayerUp {
			get { return _layerUp ?? (_layerUp = new BaseCommand(x => PlayerUtil.LayerUp())); }
		}
		#endregion
		
		#region удалить слой
		private static BaseCommand _layerDelete;
		public static ICommand LayerDelete {
			get { return _layerDelete ?? (_layerDelete = new BaseCommand(x => PlayerUtil.LayerDelete())); }
		}
		#endregion

		#endregion

		#region Селекторы

		#region выделение фреймов с контролом
		private static BaseCommand _selectManyFrame;
		public static ICommand SelectManyFrame {
			get { return _selectManyFrame ?? (_selectManyFrame = new BaseCommand(x => Selectors.SelMultiFrame(x, true))); }
		}
		#endregion

		#region выделение фреймов с с шифтом
		private static BaseCommand _selectShiftFrame;
		public static ICommand SelectShiftFrame {
			get { return _selectShiftFrame ?? (_selectShiftFrame = new BaseCommand(Selectors.SelShiftFrame)); }
		}
		#endregion

		#region диалог свойств фрейма
		private static BaseCommand _doubleClickFrame;
		public static ICommand DoubleClickFrame {
			get { return _doubleClickFrame ?? (_doubleClickFrame = new BaseCommand(Selectors.EnterTrackFrame)); }
		}
		#endregion

		#endregion

		#region прошивка
		#region обновление списка устройств
		private static BaseCommand _refreshController;
		public static ICommand RefreshController {
			get { return _refreshController ?? (_refreshController = new BaseCommand(x => FlashUtil.RefreshControllers())); }
		}
		#endregion

		#region прошивка контроллеров
		private static BaseCommand _flashController;
		public static ICommand FlashController {
			get { return _flashController ?? (_flashController = new BaseCommand(x => FlashUtil.FlashControllers())); }
		}
		#endregion

		#region прошивка контроллеров
		private static BaseCommand _powerController;
		public static ICommand PowerController {
			get { return _powerController ?? (_powerController = new BaseCommand(x => FlashUtil.PowerController())); }
		}
		#endregion
		#endregion
	}
}
