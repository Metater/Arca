arc vars
vars int count 42
vars.count string message "Something dynamically put in an archetype"
vars.count.message.println
vars.count.add 1
vars!.count.println