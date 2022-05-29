// For more information see https://aka.ms/fsharp-console-apps

open AlgEff.Handler
open AlgEff.Effect

type ProgramEnv<'ret>() as this =
    inherit Environment<'ret>()

    let handler =
        Handler.combine2
            (PureLogHandler(this))
            (ActualConsoleHandler(this))
    
    interface ConsoleContext
    interface LogContext

    member __.Handler = handler

let program = effect {
    do! Console.writeln "What is your name?"
    let! name = Console.readln
    do! Console.writelnf "Hello %s" name
    do! Log.writef "Name is %s" name
    return name
}


let name, (log, Unit) =
    ProgramEnv().Handler.Run(program)
