// Learn more about F# at http://fsharp.org
open System
open WordGuesser
open WordGuesser.Config

[<EntryPoint>]
let main argv =
    let words = Config.WORDS
    let rnd = System.Random()
    printfn "\nWelcome to Word Guesser"
    
    while true do
        let randomIndex = rnd.Next(words.Length)
        let answer = words.[randomIndex]
        let pLength = words.[randomIndex].Length


        printfn "\nThe length of the word is %d" pLength
        let mutable name = ""
        for i in 1..pLength do
            name <- name + Config.HIDDEN.ToString()


        printf "%s \t Used: [] Guess: " name
        let mutable used = []
        let mutable game = 1

        while game = 1 do
            let mutable testName = ""
            let s = Console.ReadKey().KeyChar.ToString()    

            if List.contains s used then
                printfn "\nYou have already guessed on this letter"
            else
                used <- used @[s]

                for i in 0..pLength-1 do
                    if  name.[i].ToString() = "*" then
                        if  answer.[i].ToString() = s then
                            testName <- testName + s
                        else
                            testName <- testName + "*"
                    else
                        testName <- testName + name.[i].ToString()
                name <- testName

                let usedList = System.String.Concat(used)

                if name = answer then
                    printfn "\nYou have won! You used %i tries!  \n\n\nNew game!" used.Length
                    game <- 0
                else
                    printf "\n%s \t Used: [%s] Guess: " name usedList



    0 // return an integer exit code
