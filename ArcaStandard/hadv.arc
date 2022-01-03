arc vars
vars int count 42
vars.count string message "Something dynamically put in an archetype"
vars.count.message.println
int bucket
vars.count.add 1 > bucket
// adding a function to vars.count
vars.count fn test
{
	arc locals
	locals string message "Count printing"
	locals.message.println
	// locals released by reaching end of block
}
vars.count.test
vars.count.println

// vars released by reaching end of code