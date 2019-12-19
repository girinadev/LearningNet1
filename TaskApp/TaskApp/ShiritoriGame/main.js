my_shiritori = Shiritori.new();

console.log(my_shiritori.play("apple")); //➞ ["apple"]
console.log(my_shiritori.play("ear")); //➞ ["apple", "ear"]
console.log(my_shiritori.play("rhino")); //➞ ["apple", "ear", "rhino"]
console.log(my_shiritori.play("corn")); //➞ "game over"

// Corn does not start with an "o".

console.log(my_shiritori.words); //➞  ["apple", "ear", "rhino"]

// Words should be accessible.

console.log(my_shiritori.restart()); //➞ "game restarted"
console.log(my_shiritori.words); //➞ []

// Words array should be set back to empty.

console.log(my_shiritori.play("hostess")); //➞ ["hostess"]
console.log(my_shiritori.play("stash")); //➞ ["hostess", "stash"]
console.log(my_shiritori.play("hostess")); //➞ "game over"

// Words cannot have already been said.

console.log(my_shiritori.play("apple")); //➞ ["apple"]
console.log(my_shiritori.play("ear")); //➞ ["apple", "ear"]
console.log(my_shiritori.play("rhino")); //➞ ["apple", "ear", "rhino"]
console.log(my_shiritori.play("ocelot")); //➞ ["apple", "ear", "rhino", "ocelot"]
console.log(my_shiritori.play("oops")); //➞ "game over"
console.log(my_shiritori.words); //➞ ["apple", "ear", "rhino", "ocelot"]
console.log(my_shiritori.game_over); //➞ true
