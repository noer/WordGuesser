// Learn more about F# at http://fsharp.org
open System
open System
open WordGuesser
open Functions

[<EntryPoint>]
let main argv =
    let words = Config.WORDS
    let rnd = System.Random()
    printfn "\nWelcome to Word Guesser"
    
    while true do
        let randomIndex = rnd.Next(words.Length)
        let answer = if Config.CASE_SENSITIVE then words.[randomIndex] else words.[randomIndex].ToLower() 
        let pLength = words.[randomIndex].Length


        printfn "\nThe length of the word is %d" pLength
        let mutable name = ""
        for i in 1..pLength do
            name <- name + Config.HIDDEN.ToString()


        printf "%s \t Used: [] Guess: " name
        let mutable used = []
        let mutable game = 1

            
        while game = 1 do
            
            let mutable inputStr = ""
            let mutable loop = true
            while loop do
                let input = Console.ReadKey(true)
                if input.Modifiers.HasFlag(ConsoleModifiers.Control) && input.Key.Equals(ConsoleKey.G) then
                    inputStr <- help answer used
                    loop <- false
                else if Config.MULTIPLE && not (input.Key.Equals(ConsoleKey.Enter)) then
                    inputStr <- inputStr + input.KeyChar.ToString()
                else if input.Key.Equals(ConsoleKey.Enter) then
                    loop <- false
                else if not (input.Modifiers.HasFlag(ConsoleModifiers.Control)) then
                    inputStr <- inputStr + input.KeyChar.ToString()
                    loop <- false
            
            //let input = if Config.MULTIPLE then Console.ReadLine() else Console.ReadKey().KeyChar.ToString()
            let s = if Config.CASE_SENSITIVE then inputStr else inputStr.ToLower()

            if List.contains s used then
                printfn "\nYou have already guessed on this letter"
            else
                used <- used @[s]

                name <- guess answer used

                let usedList = System.String.Join(" ", used)

                printf "\n%s \t Used: [%s] Guess: " name usedList
                if name = answer then
                    printfn "\nYou have won! You used %i tries!  \n\n\nNew game!" used.Length
                    game <- 0



    0 // return an integer exit code
