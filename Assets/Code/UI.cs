using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour {

    // Text fields
    [SerializeField] private UIText _day;
    [SerializeField] private UIText _subtitle;

    // Singleton
    private static UI _inst;
    public static UI Instance {
        get {
            if (_inst == null)
                _inst = Object.FindObjectOfType<UI>();
            return _inst;
        }
    }

    public void SetSubtitle(string message) {
        _subtitle.SetText(message, 3.0f, 1.0f);
    }

    public void SetDay(int day) {
        _day.SetText("Day " + day, 3.0f, 1.0f);
    }

	void Start() {
        _inst = this;
	}
	
	void Update() {
	}

    // Helper class for handling text state
    [System.Serializable]
    private class UIText {
        public Text _text;
        private Coroutine co;

        public void SetText(string message, float start, float duration) {
            if (co != null) {
                _text.StopCoroutine(co);
            }

            _text.enabled = true;
            _text.color = Color.white;
            _text.text = message;
            co = _text.StartCoroutine(FadeText(start, duration));
        }

        private IEnumerator FadeText(float start, float duration) {
            yield return new WaitForSeconds(start);

            float elapsed = 0.0f;
            while(elapsed < duration) {
                elapsed += Time.deltaTime;
                var a = 1.0f - (elapsed / duration);
                _text.color = new Color(1,1,1, a);
                yield return null;
            }

            _text.enabled = false;
        }
    }

}

