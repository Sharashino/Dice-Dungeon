using UnityEngine;
using TMPro;

// Displays desired message to game log window
public class GameLog : MonoBehaviour
{
	// TODO object displaying text message to log
	[Header("Data")]
	[SerializeField] private Color logColor = Color.yellow;
	[SerializeField] private string logMessage = "Log message";

	[Header("Object Fields")]
	[SerializeField] private TMP_Text logText;
	
	public void LogMessage(string message, Color color)
	{
		logText.text = message;
		logText.color = color;
	}
}