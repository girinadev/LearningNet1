class Shiritori {
  constructor() {
    this.words = [];
    this.game_over = false;
  }

  play(word) {
    if (word && word.length > 0 && !this.isWordExist(word) && this.isWordMatchPrevWord(word)) {
      this.words.push(word);
      return this.words;
    }

    this.game_over = true;
    return "game over";
  }

  restart() {
    this.words = [];
    this.game_over = false;

    return 'game restarted';
  }

  isWordExist(word) {
    return this.words.includes(word);
  }

  isWordMatchPrevWord(word) {
    if (this.words.length <= 0)
      return true;

    const lastWord = this.words[this.words.length - 1];
    return lastWord[lastWord.length - 1] === word[0];
  }
}

Shiritori.new = function () {
  return new Shiritori();
}