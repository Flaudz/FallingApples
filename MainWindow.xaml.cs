using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace FallingApples
{
	public partial class MainWindow : Window
	{
		BitmapImage characterLeftImg = new BitmapImage(new Uri("C:/Users/nico936d/Documents/S-2/Uge_33/Torsdag_FallingApples/FallingApples/GameCarecterLeft.png"));
		BitmapImage characterRightImg = new BitmapImage(new Uri("C:/Users/nico936d/Documents/S-2/Uge_33/Torsdag_FallingApples/FallingApples/GameCarecterRight.png"));
		public List<Image> apples = new List<Image>();
		public MainWindow()
		{
			MediaPlayer startSound = new MediaPlayer();
			startSound.Open(new Uri(@"C:\Users\nico936d\Documents\S-2\Uge_33\Torsdag_FallingApples\FallingApples\startSound.mp3"));
			startSound.Play();
			InitializeComponent();
			wait();
			this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
			apples.Add(apple1);
			apples.Add(apple2);
			apples.Add(apple3);
		}
		private void OnButtonKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Left:
					leftMovement();
					break;
				case Key.Right:
					rightMovement();
					break;
			}
		}
		private void leftMovement()
		{
			characterImg.Source = characterLeftImg;
			double characterPos = Canvas.GetLeft(characterImg);
			if (characterPos > 0)
			{
				Canvas.SetLeft(characterImg, characterPos - 10);
			}
		}
		private void rightMovement()
		{
			characterImg.Source = characterRightImg;
			double characterPos = Canvas.GetLeft(characterImg);
			if (characterPos! < (ActualWidth - 65))
			{
				Canvas.SetLeft(characterImg, characterPos + 10);
			}
		}
		public int i;
		private void fallingApple()
		{
			for (i = 0; i < apples.Count; i++)
			{
				Random rnd = new Random();
				double AppleTop = Canvas.GetTop(apples[i]);
				double AppleLeft = Canvas.GetLeft(apples[i]);
				double characterPos = Canvas.GetLeft(characterImg);
				int rndTopNumber = rnd.Next(0, 200);
				int rndTop = rndTopNumber - (rndTopNumber * 2);
				Canvas.SetTop(apples[i], AppleTop + 7);
				if (AppleTop == 0)
				{
					Canvas.SetLeft(apples[i], rnd.Next(0, (int)((int)ActualWidth - apples[0].ActualWidth)));
				}
				if (AppleTop >= ActualHeight)
				{
					Canvas.SetTop(apples[i], rndTop);
				}
				if (AppleTop >= this.ActualHeight - (characterImg.ActualHeight * 2))
				{
					if (AppleLeft < characterPos + 25 && AppleLeft > characterPos - 25)
					{
						Canvas.SetTop(apples[i], rndTop);
						int score = int.Parse(scoreText.Text) + 1;
						scoreText.Text = score.ToString();
						MediaPlayer player = new MediaPlayer();
						player.Open(new Uri(@"C:\Users\nico936d\Documents\S-2\Uge_33\Torsdag_FallingApples\FallingApples\GOTEMM.mp3"));
						player.Play();
					}
				}
				if (i == apples.Count - 1)
				{
					wait();
				}
			}
		}
		//Wait
		async void wait()
		{
			await Task.Delay(50);
			fallingApple();
		}
	}
}