class Battleship {

	charSet = {
	  "blank": '○',
	  "ship": '●',
	  "miss": '☼',
	  "hit": '☀'
	};
  
	boardSet = new Array();
  
	constructor(scheme, input) {
	  this.createBoard();
	  this.applySchemeToBoard(scheme);
	  this.applyInput(input);
	}
  
	board() {
	  return this.boardSet;
	}
  
	hits() {
	  let count = 0;
	  this.boardSet.forEach((r, i) => {
		count += r.filter(e => e == this.charSet.hit).length;
	  });
  
	  return count;
	}
  
	sunk() {
	  let count = 0;
  
	  for (let i = 0; i < this.boardSet.length; i++) {
		for (let j = 0; j < this.boardSet[i].length; j++) {
		  if (this.boardSet[i][j] !== this.charSet.hit)
			continue;
  
		  if ((this.boardSet[i - 1] && this.boardSet[i - 1][j] == this.charSet.hit) ||
			(this.boardSet[i + 1] && this.boardSet[i + 1][j] == this.charSet.hit))
			count++;
		  else if ((this.boardSet[i][j - 1] == this.charSet.hit) ||
			(this.boardSet[i][j + 1] == this.charSet.hit))
			count++;
		}
	  }
  
	  return count / 2;
	}
  
	points() {
	  return 1 * this.hits() + 2 * this.sunk();
	}
  
	  createBoard() {
	  this.boardSet = new Array(5);
	  for (let i = 0; i < this.boardSet.length; i++) {
		this.boardSet[i] = (new Array(5)).fill(this.charSet.blank);
	  }
	}
  
	applyInput(inArray) {
	  this.getBoardIndexes(inArray).forEach((item) => {
		const i = item[0];
		const j = item[1];
		if (this.boardSet[i][j] == this.charSet.ship)
		  this.boardSet[i][j] = this.charSet.hit;
		else if (this.boardSet[i][j] == this.charSet.blank)
		  this.boardSet[i][j] = this.charSet.miss;
	  });
	}
  
	applySchemeToBoard(inArray) {
	  this.getBoardIndexes(inArray).forEach((item) => {
		const i = item[0];
		const j = item[1];
		this.boardSet[i][j] = this.charSet.ship;
	  });
	}
  
	getBoardIndexes(inArray) {
	  let mapSymbol = s => {
		if (s == 'A') return 0;
		if (s == 'B') return 1;
		if (s == 'C') return 2;
		if (s == 'D') return 3;
		if (s == 'E') return 4;
  
		return Number(s) - 1;
	  };
  
	  return inArray.map((currentValue, index, array) => {
		return [mapSymbol(currentValue[0]), mapSymbol(currentValue[1])];
	  });
	}  
  
  }