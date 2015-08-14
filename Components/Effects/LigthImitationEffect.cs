using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LEDX.Components.Effects {

	public class LigthImitationEffect : ShaderEffect {

		private static string _assemblyShortName;
		private static readonly PixelShader _pixelShader = new PixelShader();

		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(LigthImitationEffect), 0);

		public static readonly DependencyProperty ColorToneProperty = DependencyProperty.Register("ColorTone", typeof(Color), typeof(LigthImitationEffect), new UIPropertyMetadata(Colors.White, PixelShaderConstantCallback(0)));

		static LigthImitationEffect() {
			_pixelShader.UriSource = MakePackUri("Effects/LigthImitation2.ps");
		}

		public LigthImitationEffect() {
			PixelShader = _pixelShader;
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(ColorToneProperty);
		}

		public static Uri MakePackUri(string relativeFile) {
			string path = "pack://application:,,,/" + AssemblyShortName + ";component/" + relativeFile;
			return new Uri(path, UriKind.RelativeOrAbsolute);
		}

		private static string AssemblyShortName {
			get { return _assemblyShortName ?? (_assemblyShortName = typeof(LigthImitationEffect).Assembly.ToString().Split(',')[0]); }
		}

		public Brush Input {
			get {
				return (Brush)GetValue(InputProperty);
			}
			set {
				SetValue(InputProperty, value);
			}
		}

		public Color ColorTone {
			get {
				return (Color)GetValue(ColorToneProperty);
			}
			set {
				SetValue(ColorToneProperty, value);
			}
		}
	}
}
