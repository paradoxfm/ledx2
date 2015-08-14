using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LEDX.Components.Effects {
	
	public class LightCreate : ShaderEffect {
		private static string _assemblyShortName;
		private static readonly PixelShader _pixelShader = new PixelShader();

		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(LightCreate), 0);

		static LightCreate() {
			_pixelShader.UriSource = MakePackUri("Effects/LightCreate.ps");
		}

		public LightCreate() {
			PixelShader = _pixelShader;
			UpdateShaderValue(InputProperty);
		}

		public static Uri MakePackUri(string relativeFile) {
			string path = "pack://application:,,,/" + AssemblyShortName + ";component/" + relativeFile;
			return new Uri(path, UriKind.RelativeOrAbsolute);
		}

		private static string AssemblyShortName {
			get { return _assemblyShortName ?? (_assemblyShortName = typeof (LightCreate).Assembly.ToString().Split(',')[0]); }
		}

		public Brush Input {
			get {
				return (Brush)GetValue(InputProperty);
			}
			set {
				SetValue(InputProperty, value);
			}
		}
	}
}
