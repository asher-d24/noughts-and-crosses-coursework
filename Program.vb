
Imports System
Imports System.IO

Module Program

    Sub Main()

        Dim p1 As String = ""
        Dim p2 As String = ""
        Dim symbol1 As String  = ""
        Dim symbol2 As String= ""
        Dim choice As String= ""
        Dim check As Boolean = False

        startgame(p1, p2)

        While check = False

            Console.WriteLine("Pick a choice")
            Console.WriteLine("1: Play a game")
            Console.WriteLine("2: Load a game")
            Console.WriteLine("3: Save a game")

            choice = Console.ReadLine()

            If choice = "1" Then
                check = True
                playgame(p1, p2, symbol1, symbol2)

            ElseIf choice = "2" Then
                check = True
                loadaWin()

            ElseIf choice = "3" Then
                check = True
                

            Else
                Console.WriteLine("Type a number from 1, 2 or 3")

            End If

        End While

    End Sub


    Sub startgame(ByRef p1 As String, ByRef p2 As String)

        Console.WriteLine("Enter player one's name")
        p1 = Console.ReadLine()

        Console.WriteLine("Enter player two's name")
        p2 = Console.ReadLine()

        Console.WriteLine("Hello " & p1 & " and " & p2 &
                          ", I am the Noughts & Crosses program")

    End Sub


    Sub playgame(ByRef p1 As String, ByRef p2 As String,
                 ByRef symbol1 As String, ByRef symbol2 As String)

        Dim board(2, 2) As String
        dim gameWon As Boolean = False

        ' initialise the board
        For row As Integer = 0 To 2
            For col As Integer = 0 To 2
                board(row, col) = "-"
            Next
        Next

        Console.WriteLine("You are in playgame")

        PickSymbols(p1, p2, symbol1, symbol2)

        displaygrid(board)

        ' allow both players to make a move
        For turn As Integer = 1 To 9

            If turn Mod 2 = 1 Then
                console.WriteLine(p1 & "'s turn")
                placeSymbol(p1, symbol1, board)

                gameWon = checkForWin(p1, symbol1, board)
            Else
                console.WriteLine(p2 & "'s turn")
                placeSymbol(p2, symbol2, board)

                gameWon = checkForWin(p2, symbol2, board)
            End If

            displaygrid(board)

            if gameWon = true then
                saveAWin(board) 
                exit for
            end if

        next

        if  gameWon= false then
            console.WriteLine("Game over and its a draw!")
        end if

    End Sub


    Sub PickSymbols(ByRef p1 As String, ByRef p2 As String,
                    ByRef symbol1 As String, ByRef symbol2 As String)

        Dim check As Boolean = False

        Do Until check = True

            Console.WriteLine(p1 & " pick a one letter symbol")
            symbol1 = Console.ReadLine()

            Console.WriteLine(p2 & " pick a one letter symbol")
            symbol2 = Console.ReadLine()

            If String.IsNullOrWhiteSpace(symbol1) OrElse
               String.IsNullOrWhiteSpace(symbol2) Then

                Console.WriteLine("Character cannot be empty")

            ElseIf symbol1.Length <> 1 OrElse symbol2.Length <> 1 Then

                Console.WriteLine("Character must be a single character or symbol")

            ElseIf symbol1 = symbol2 Then

                Console.WriteLine("Choose a different symbol from the other player")

            Else

                check = True

            End If

        Loop

    End Sub


    Sub displaygrid(ByRef board(,) As String)

        Console.WriteLine("  1 2 3")

        For row As Integer = 0 To 2

            Console.Write((row + 1) & " ")

            For col As Integer = 0 To 2
                Console.Write(board(row, col) & " ")
            Next

            Console.WriteLine()

        Next

    End Sub


    
    Sub placeSymbol(ByVal name As String, ByVal symbol As String,
                ByRef board(,) As String)

        Dim row As Integer
        Dim col As Integer
        Dim valid As Boolean = False

        Do Until valid = True
            Console.WriteLine(name & ", enter column number (1-3)")
            If Integer.TryParse(Console.ReadLine(), col) = False Then
                Console.WriteLine("invalid input Please enter a number.")
            ElseIf col < 1 Or col > 3 Then
                Console.WriteLine("Column must be between 1 and 3")
            Else
                Console.WriteLine(name & ", enter row number (1-3)")
                If Integer.TryParse(Console.ReadLine(), row) = False Then
                    Console.WriteLine("Invalid input  please enter a number")
                ElseIf row < 1 Or row > 3 Then
                    Console.WriteLine("Row must be between 1 and 3")
                ElseIf board(row - 1, col - 1) <> "-" Then
                    Console.WriteLine("That position is already occupied choose another")
                Else
                    board(row - 1, col - 1) = symbol
                    
                valid = True
                    
                End If
            End If
        Loop

    End Sub

    Function checkForWin(ByVal name As String, ByVal symbol As String, ByRef board(,) As String) As Boolean

        'check rows
        for row as integer = 0 to 2
            if board(row, 0) = symbol andalso board(row, 1) = symbol andalso board(row, 2) = symbol then
                console.writeline(name & " wins!")
                return true
            end if
        next

        'check columns
        for col as integer = 0 to 2
            if board(0, col) = symbol andalso board(1, col) = symbol andalso board(2, col) = symbol then
                console.writeline(name & " wins!")
                return true
            end if
        next        

        'check diagonals top left too bottom right
        if board(0, 0) = symbol andalso board(1, 1) = symbol andalso board(2, 2) = symbol then
            console.writeline(name & " wins!")
            return true
        end if

        'check diagonals top right too bottom left
        if board(0, 2) = symbol andalso board(1, 1) = symbol andalso board(2, 0) = symbol then
            console.writeline(name & " wins!")
            return true
        end if  

        return false
    end function



    


    Sub loadaWin()

        Console.WriteLine("You are in loadawin")

    End Sub


    Sub saveAWin(byref board(,) as string)
        Console.WriteLine("You are in saveawin")
        dim filePath as string = "winningGame.txt"

        using writer as new StreamWriter(filePath)
            for row as integer = 0 to 2
                for col as integer = 0 to 2
                    writer.Write(board(row, col))
                
                    if col<2 then
                        writer.Write(", ")
                    end if
                next

                writer.writeline()

            next

        end using
        console.writeline("Winning game saved to " & filePath)
    End Sub


End Module
