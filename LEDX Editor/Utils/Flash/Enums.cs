
namespace LEDX.Utils.Flash {

	class FEdge {
		/// <summary>Стартовый байт</summary>
		public const byte Start = 0xAA;
		/// <summary>Стоповый байт</summary>
		public const byte Stop = 0x55;
	}

	class FType {
		/// <summary>Запрос</summary>
		public const byte Req = (byte)'R';
		/// <summary>Подтверждение</summary>
		public const byte Acs = (byte)'C';
		/// <summary>Сигнал</summary>
		public const byte Syg = (byte)'s';
	}

	class FCmd {
		/// <summary>Проверка связи</summary>
		public const byte Echo = 0x00;
		/// <summary>Установка цвета</summary>
		public const byte SetColor = 0x01;
		/// <summary>Показать цвет из набора</summary>
		public const byte ShowColorSet = 0x02;
		/// <summary>Запустить анимацию</summary>
		public const byte RunProg = 0x03;
		/// <summary>Пауза анимации</summary>
		public const byte Pause = 0x04;
		/// <summary>Управление питанием</summary>
		public const byte Power = 0x05;
		/// <summary>Установить скорость</summary>
		public const byte ChangeSp = 0x06;
		/// <summary>Установить яркость</summary>
		public const byte ChangeBr = 0x07;
		/// <summary>Начать синхронизацию. С компьютера не используется</summary>
		public const byte StartSync = 0x08;
		/// <summary>Закончить синхронизацию. С компьютера не используется</summary>
		public const byte StopSync = 0x09;
		/// <summary>Синхро команда. С компьютера не используется</summary>
		public const byte Sync = 0x0A;
		/// <summary>Запрос состояния контроллера</summary>
		public const byte GetState = 0x0B;
		/// <summary>Войти режим программирования</summary>
		public const byte EnterProgMode = 0x0C;
		/// <summary>Записать фрейм программы</summary>
		public const byte WriteFrame = 0x0D;
		/// <summary>//Завершить программу и выйти из режима программирования</summary>
		public const byte FinilizeProg = 0x0E;
	}

	class FErr {
		/// <summary>Ошибки нет. Команда выполнена корректно</summary>
		public const byte ErrOk = 0x01;
		/// <summary>Ошибка контрольной суммы пакета</summary>
		public const byte ErrChs = 0x02;
		/// <summary>Поступила неизвестная команда</summary>
		public const byte ErrUcom = 0x03;
		/// <summary>Поступила команда, неожиданная в данном режиме</summary>
		public const byte ErrNec = 0x04;
		/// <summary>Ошибка заполнения памяти</summary>
		public const byte ErrMemf = 0x05;
	}
}
