
Imports System
Imports System.IO

Module Program

    ' main subroutine that runs the program
    Sub Main()
        Dim board(2, 2) as string
        Dim p1 As String = ""
        Dim p2 As String = ""
        Dim symbol1 As String  = ""
        Dim symbol2 As String= ""
        Dim choice As String= ""
        Dim check As Boolean = False

        ' ask both players for their names
        startgame(p1, p2)

        ' keep showing the menu until the user chooses to exit
        While check = False

            Console.WriteLine("Pick a choice")
            Console.WriteLine("1: Play a game")
            Console.WriteLine("2: Load saved games")
            Console.WriteLine("3: Save a game")
            Console.WriteLine("4: Play a game with a computer")
            console.WriteLine("5: Exit the program")

            ' read the menu option chosen by the user
            choice = Console.ReadLine()
            
                If choice = "1" Then
                    
                    ' start a game between two players
                    playgame(p1, p2, symbol1, symbol2, board)

                ElseIf choice = "2" Then
                    
                    ' load and display saved games
                    loadaWin()

                ElseIf choice = "3" Then
                    
                    ' save the current board to a file
                    saveAWin(board)
                
                ElseIf choice = "4" Then
                    ' start a game against the computer
                    playAgainstComputer(board)
                    
                Elseif choice = "5" Then
                    ' stop the menu loop
                    check = True
                    Console.WriteLine("Thansk for playing ")
                else
                    Console.WriteLine("Invalid choice, please choose again")

                End If
            

        End While

    End Sub


    ' get the names of the two players
    Sub startgame(ByRef p1 As String, ByRef p2 As String)

        Console.WriteLine("Enter player one's name")
        ' store player ones name
        p1 = Console.ReadLine()

        Console.WriteLine("Enter player two's name")
        ' store player twos name
        p2 = Console.ReadLine()

        Console.WriteLine("Hello " & p1 & " and " & p2 &
                          ", I am the Noughts & Crosses program")

    End Sub


    ' run a game between two human players
    Sub playgame(ByRef p1 As String, ByRef p2 As String,
                 ByRef symbol1 As String, ByRef symbol2 As String, ByRef board(,) As String)

        ' keep track of whether someone has won
        Dim gameWon As Boolean = False

        ' initialise the board
        For row As Integer = 0 To 2
            ' display each square in the current row
            For col As Integer = 0 To 2
                board(row, col) = "-"
            Next
        Next

        Console.WriteLine("You are in playgame")

        ' ask each player to choose a symbol
        PickSymbols(p1, p2, symbol1, symbol2)

        ' show the current board
        displaygrid(board)

        For turn As Integer = 1 To 9

            ' odd turns belong to player one and even turns to player two
            If turn Mod 2 = 1 Then
                console.WriteLine(p1 & "'s turn")
                ' let player one choose a position
                placeSymbol(p1, symbol1, board)

                gameWon = checkForWin(p1, symbol1, board)
            Else
                console.WriteLine(p2 & "'s turn")
                ' let player two choose a position
                placeSymbol(p2, symbol2, board)

                gameWon = checkForWin(p2, symbol2, board)
            End If

            displaygrid(board)

            ' stop the game if a player has won
            if gameWon = true then
                exit for
            end if

        next

        if  gameWon= false then
            console.WriteLine("Game over and its a draw!")
        end if

    End Sub

    ' run a game between the player and the computer
    sub playAgainstComputer(ByRef board(,) As String)
        Dim playerSymbol As String = ""
        Dim computerSymbol As String = ""
        Dim valid As Boolean = False

        ' keep asking until a valid empty square is chosen
        Do Until valid = True

            console.WriteLine("Choose your symbol")
            playerSymbol = Console.ReadLine()

            console.WriteLine("Choose the computers symbol")
            computerSymbol = Console.ReadLine()

            ' check that neither symbol is empty
            If String.IsNullOrWhiteSpace(playerSymbol) OrElse
                String.IsNullOrWhiteSpace(computerSymbol) Then

                Console.WriteLine("Symbols cant be empty")

            ' check that both symbols contain one character
            ElseIf playerSymbol.Length <> 1 OrElse computerSymbol.Length <> 1 Then

                Console.WriteLine("Both symbols must be one character")

            ' make sure the symbols are different
            ElseIf playerSymbol = computerSymbol Then

                Console.WriteLine("Choose different symbols")

            Else
                valid = True

            End If
        loop

            ' reset every square before starting the game
            For row As Integer = 0 To 2
                For col As Integer = 0 To 2
                    board(row, col) = "-"
                Next
            Next

            displaygrid(board)

            Dim gameWon As Boolean = False
            ' create a random number generator for the computers moves
            Dim random As New Random()
            Dim computerRow As Integer
            Dim computerCol As Integer

            ' allow up to nine moves on the board
            For turns As Integer = 1 To 9
                If turns Mod 2 = 1 Then
                    console.WriteLine("your turn")
                    ' let the player make a move
                    placeSymbol("Player", playerSymbol, board)
                    gameWon = checkForWin("Player", playerSymbol, board)
                Else
                    console.WriteLine("Computer's turn")
            
                    ' keep choosing positions until an empty square is found
                    Do 
                        computerRow = random.Next(0, 3)
                        computerCol = random.Next(0, 3)
                    Loop until board(computerRow, computerCol) = "-"

                    ' place the computer symbol in the chosen square
                    board(computerRow, computerCol) = computerSymbol
                    gameWon = checkForWin("Computer", computerSymbol, board)
                    console.WriteLine("Computer placed " & computerSymbol & " at row " & (computerRow + 1) & ", column " & (computerCol + 1))
                End If

                displaygrid(board)

            If gameWon = True Then
                exit for
            End If
        next

        if gameWon = false then
            console.WriteLine("Game over and its a draw!")
        end if
    end sub


    ' ask both players to choose different one-character symbols
    sub PickSymbols(ByRef p1 As String, ByRef p2 As String,
                    ByRef symbol1 As String, ByRef symbol2 As String)

        Dim check As Boolean = False

        ' repeat until the symbols pass all the checks
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


    ' display the board with row and column numbers
    sub displaygrid(ByRef board(,) As String)

        Console.WriteLine("  1 2 3")

        ' go through each row of the board
        For row As Integer = 0 To 2

            Console.Write((row + 1) & " ")

            ' display each square in the current row
            For col As Integer = 0 To 2
                Console.Write(board(row, col) & " ")
            Next

            Console.WriteLine()

        Next

    End Sub


    
    ' ask a player where they want to place their symbol
    sub placeSymbol(ByVal name As String, ByVal symbol As String,
                ByRef board(,) As String)

        Dim row As Integer
        Dim col As Integer
        Dim valid As Boolean = False

        ' keep asking until a valid empty square is chosen
        Do Until valid = True
            Console.WriteLine(name & ", enter column number (1-3)")
            ' check that the column input is a number
            If Integer.TryParse(Console.ReadLine(), col) = False Then
                Console.WriteLine("invalid input Please enter a number.")
            ElseIf col < 1 Or col > 3 Then
                Console.WriteLine("Column must be between 1 and 3")
            Else
                Console.WriteLine(name & ", enter row number (1-3)")
                ' check that the row input is a number
                If Integer.TryParse(Console.ReadLine(), row) = False Then
                    Console.WriteLine("Invalid input, please enter a number")
                ElseIf row < 1 Or row > 3 Then
                    Console.WriteLine("Row must be between 1 and 3")
                ' check whether the chosen square is already taken
                ElseIf board(row - 1, col - 1) <> "-" Then
                    Console.WriteLine("That position is already occupied choose another")
                Else
                    ' place the players symbol in the chosen square
                    board(row - 1, col - 1) = symbol
                    
                valid = True
                    
                End If
            End If
        Loop

    End sub

    ' check if the given symbol has made a winning line
    function checkForWin(ByVal name As String, ByVal symbol As String, ByRef board(,) As String) As Boolean

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

        ' return false if no winning line was found
        return false
    end function



    


    ' load a saved game from a file chosen by the user
    sub loadaWin()

        dim board(2,2) as string
        dim filepath as string
        console.writeline("Enter the file path of the winning game you want to load")
        ' get the file path entered by the user
        filepath = console.readline()
        ' check that the file exists before opening it
        if file.exists(filepath) then
            console.writeline("Loading winning game from " & filepath)
            ' open the file so its saved contents can be read
            using reader as new streamreader(filepath)
                dim line as string
                do
                    ' read the next line from the file
                    line = reader.readline()
                    if line isnot nothing then
                        console.writeline(line)
                    end if
                loop until line is nothing
            end using
        else
            console.writeline("File does not exist")
        end if

    end sub


    ' save the current board to a text file
    sub  saveAWin(byref board(,) as string)
        dim filePath as string = "winningGame.txt"
        Console.WriteLine("Saving to: " & Path.GetFullPath(filePath))
        ' open the file in append mode so earlier games are kept
        using writer as new StreamWriter(filePath,true)
            for row as integer = 0 to 2
                for col as integer = 0 to 2
                    ' write each square of the board to the file
                    writer.Write(board(row, col))
                
                    if col<2 then
                        writer.Write(", ")
                    end if
                next

                writer.writeline()

            next
            ' add divider between saved games
            writer.writeline("-----------")
        end using
        console.writeline("Winning game saved to " & filePath)
    end sub


End Module
