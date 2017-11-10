open System
open System.Windows.Forms
open System.Drawing
open System.IO
open System.Diagnostics
//open System.Media
//open System.Windows.Forms.DataGridViewComboBoxCell

//System.Diagnostics.Process.Start("MyCommand", "arg1, arg2, arg3");


let debug_window_size_key_press(f : System.Drawing.Size, e : KeyEventArgs) = 
        match e with
        | e when e.KeyCode = Keys.C -> Console.WriteLine("Form Width: {0}\nForm Height: {1}", f.Width, f.Height)
        | e when e.KeyCode = Keys.K -> Console.WriteLine("erwrewrf")
        | _  -> ignore()


// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.


// main form


let mainFileExit_Click(sender : System.Object, e : EventArgs) = 
        exit 0 |> ignore



//let stuffToDoWhenKeyPressed s (e : KeyEventArgs) = 
let debug_window_size(sender : System.Drawing.Size) = 
        Console.Clear()
        Console.WriteLine("Form Width: {0}\nForm Height: {1}",sender.Width, sender.Height)
        //match e with
        //| e when Char.ToLower e.KeyChar = 'q' -> Console.WriteLine("Form Width: {0}\nForm Height: {1}",sender.Width, sender.Height)
        //| _ -> ignore()
        


let directoryButton_Click (tbtBox : TextBox, lstBox: ListBox, size: String, e : EventArgs) = 
        let folderBrowser = new FolderBrowserDialog()
        let browserResult = folderBrowser.ShowDialog()
        //let browserMatch = if browserResult = DialogResult.OK then tbtBox.Text <- folderBrowser.SelectedPath
        //Array.iter ( fun a -> lstBox.Items.Add(a) ) Directory.GetFiles(tbtBox.Text)
        //|>ignore

        //Array.iter (fun x -> printf "%d " x)
        //|>lstBox.Items.AddRange

        if browserResult = DialogResult.OK then ignore()
        //if folderBrowser.ShowDialog()  DialogResult.OK then
        //    sender.Text = folderBrowser.SelectedPath
        //|> ignore()



    //     let x = 
    //match 1 with 
    //| _ -> "z" 
    //| 1 -> "a"
    //| 2 -> "b" 
 

let randomizeButton_Click(tbxDirectory : TextBox, tbxAmount : TextBox, e : EventArgs) =
    ignore



let playButton_Click (sender : TextBox, e : EventArgs) = 
    let dir = new DirectoryInfo(sender.Text)//new DirectoryInfo(@"D:\\Sammy\\anime\\Last week - watched\\Dragon Ball Super")
    if not(String.IsNullOrEmpty(dir.FullName)) then
        let files1 = 
            Directory.GetFiles(dir.FullName)
            |>Array.map(fun s -> "\"\"\"" + s)
            |>Array.map(fun s -> s + "\"\"\"")
        let filesFinal = """/play /add """ + String.Join( " ", files1)
        System.Diagnostics.Process.Start("""C:\Program Files\MPC-HC\mpc-hc64.exe""", filesFinal) |> ignore // D:\Sammy\musica\\")



//let debug_cursorLocation_Click(sender : Point) = 
//        Console.Clear()
//        Console.Write("Cursor Location: " + sender.X.ToString() + ", " + sender.Y.ToString())



//let makeVideoPlaylistFile() = 
//    let currentDir = Directory.GetCurrentDirectory()
//    File.Create(currentDir + "video_playlist.mpcpl")


//let initVideoPlaylist(video : String[]) = 
//    let currentDir = Directory.GetCurrentDirectory()
//    let playlist_exists = System.IO.File.Exists(currentDir + "\\video_playlist.mpcpl")
//
//    if not(playlist_exists) then
//        use playlist = new StreamWriter(currentDir + "\\video_playlist.mpcpl")
//        let mutable count = 0
//        playlist.Write("MPCPLAYLIST")
//        for i in video do
//            playlist.Write(count.ToString() + ",type, 0")
//            playlist.Write(count.ToString() + ",filename," + i)
//        playlist.Close()    
        

    



[<EntryPoint>][<STAThread>]
let main (argv :string[]) = 
    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault false
 
    let form = new Form()

    let mainMenu = new MainMenu()
    let mainFile = new MenuItem("File")
    let mainFileExit = new MenuItem("Exit")

    let buttonPanel = new Panel()
    let tbxDirectory = new TextBox()
    let btnDirectory = new Button()
    let btnRandomize = new Button()
    let btnPlay = new Button()
    let cbxMusicBox = new CheckBox()
    let cbxVideoBox = new CheckBox()
    let lblAmount = new Label()
    let tbxAmount = new TextBox()

    let mediaList = new ListBox()

    //form.Width <- 1142
    //form.Height <- 750
    //form.AutoSize <- false
    form.AutoScaleDimensions <- new System.Drawing.SizeF(12.0f, 25.0f)
    form.AutoScaleMode <- System.Windows.Forms.AutoScaleMode.Font
    form.Size <- new System.Drawing.Size(598, 418)
    form.MinimumSize <- new System.Drawing.Size(598, 418)
    form.Name <- "Playlist Randomizer"
    form.Text <- "Playlist Randomizer"
    
    // main menu
 
    mainFileExit.Click.AddHandler(new System.EventHandler (fun s e -> mainFileExit_Click(s, e)))
    mainFile.MenuItems.AddRange( [| mainFileExit |] )
    mainMenu.MenuItems.AddRange( [| mainFile |] )
    form.Menu <- mainMenu

    
     

    buttonPanel.Name <- "buttonPanel"
    buttonPanel.TabIndex <- 0
    buttonPanel.Dock <- System.Windows.Forms.DockStyle.Left
    buttonPanel.Size <- new System.Drawing.Size(520, 726)
    buttonPanel.Location <- new System.Drawing.Point(12, 12)
    //buttonPanel.BackColor <- Color.Blue
    buttonPanel.Width <- form.Width/2 // trying to figure out size


    // Directory Textbox
    tbxDirectory.Location <- new System.Drawing.Point(26, 35)
    tbxDirectory.Name <- "Directory"
    tbxDirectory.Size <- new System.Drawing.Size(250, 31)
    tbxDirectory.TabIndex <- 0

    // directory button
    btnDirectory.Text <- "Directory"
    btnDirectory.Location <- new System.Drawing.Point(90, 80)
    btnDirectory.Size <- new System.Drawing.Size(120, 30)
    btnDirectory.TabIndex <- 1 //??
    btnDirectory.Click.AddHandler(new System.EventHandler (fun _ e -> directoryButton_Click(tbxDirectory, mediaList, tbxAmount.Text, e))) // change to the real function

    
    // Randomize
    btnRandomize.Location <- new System.Drawing.Point(90, 130)
    btnRandomize.Name <- "Randomize"
    btnRandomize.Size <- new System.Drawing.Size(120, 30)
    btnRandomize.Text <- "Randomize"
    btnRandomize.UseVisualStyleBackColor <- true
    btnRandomize.TabIndex <- 2
    btnPlay.Click.AddHandler(new System.EventHandler (fun _ e -> randomizeButton_Click(tbxDirectory, tbxAmount.Text, e))) // change to the real function


    // Play Button
    btnPlay.Location <- new System.Drawing.Point(90, 180)
    btnPlay.Name <- "Play";
    btnPlay.Size <- new System.Drawing.Size(120, 30)
    btnPlay.Text <- "Play";
    btnPlay.UseVisualStyleBackColor <- true;
    btnPlay.TabIndex <- 3;
    btnPlay.Click.AddHandler(new System.EventHandler (fun _ e -> playButton_Click(tbxDirectory, e))) // change to the real function

    //// debug button
    //let debugButton = new Button()
    //debugButton.Text <- "Debug"
    //debugButton.Click.AddHandler(new System.EventHandler (fun _ _ -> debug_window_size form.Size)) // change to the real function

    //form.KeyPress.AddHandler(new KeyPressEventHandler (fun f e -> debug_window_size_key_press(form.Size, e)))
    //form.KeyDown.AddHandler(new System.Windows.Forms.KeyEventHandler (fun s e -> debug_window_size_key_press(form.Size, e)))

    // videoBox check box
    cbxVideoBox.Location <- new System.Drawing.Point(200, 227)
    cbxVideoBox.Name <- "videoBox"
    cbxVideoBox.Size <- new System.Drawing.Size(100, 30)
    cbxVideoBox.Text <- "Video"
    cbxVideoBox.AutoSize <- true
    cbxVideoBox.TabIndex <- 4

    // musicBox check box
    cbxMusicBox.Location <- new System.Drawing.Point(200, 240)
    cbxMusicBox.Name <- "musicBox"
    cbxMusicBox.Size <- new System.Drawing.Size(100, 30)
    cbxMusicBox.Text <- "Music"
    cbxMusicBox.TabIndex <- 5


    lblAmount.Text <- "Size of List"
    lblAmount.TextAlign <- ContentAlignment.TopCenter
    lblAmount.Size <- new System.Drawing.Size(50,50)
    lblAmount.Location <- new System.Drawing.Point(20,230)
    lblAmount.TabIndex <- 6

    tbxAmount.Text <- "1"
    tbxAmount.Location <-new System.Drawing.Point(lblAmount.Location.X + lblAmount.Size.Width + 5,233)
    tbxAmount.Size <- new System.Drawing.Size(20, 90)
    tbxAmount.TabIndex <- 7
   
    buttonPanel.Controls.Add(tbxDirectory)
    buttonPanel.Controls.Add(btnDirectory)
    buttonPanel.Controls.Add(btnRandomize)
    buttonPanel.Controls.Add(btnPlay)
    buttonPanel.Controls.Add(cbxVideoBox)
    buttonPanel.Controls.Add(cbxMusicBox)
    buttonPanel.Controls.Add(lblAmount)
    buttonPanel.Controls.Add(tbxAmount)
    //buttonPanel.Controls.Add(debugButton)

    buttonPanel.ResumeLayout(false)
    buttonPanel.PerformLayout()
    form.ResumeLayout(false)
    form.AutoScaleMode <- System.Windows.Forms.AutoScaleMode.Font


   // media list box
    mediaList.Dock <- System.Windows.Forms.DockStyle.Fill
    mediaList.FormattingEnabled <- true
    mediaList.ItemHeight <- 25
    mediaList.Location <- new System.Drawing.Point(538, 0)
    mediaList.Name <- "MediaList";
    mediaList.Size <- new System.Drawing.Size(592, 729)
    mediaList.TabIndex <- 1

    // Background
    let Background = new Panel()
    Background.Anchor <- (System.Windows.Forms.AnchorStyles.Top ||| System.Windows.Forms.AnchorStyles.Bottom ||| (System.Windows.Forms.AnchorStyles.Left ||| System.Windows.Forms.AnchorStyles.Right))
    
    Background.Dock <- System.Windows.Forms.DockStyle.Fill;
    Background.ForeColor <- System.Drawing.SystemColors.ControlText;
    Background.Location <- new System.Drawing.Point(0, 0);
    Background.Name <- "Background";
    Background.Size <- new System.Drawing.Size(2035, 1198);
    Background.TabIndex <- 0;
    Background.Controls.Add(mediaList)
    Background.Controls.Add(buttonPanel)

    Background.Controls.AddRange([| 
                                    (buttonPanel)
                                    (mediaList)
                                |])

    Background.Focus() |> ignore
    form.Controls.AddRange([| 
                                (Background:> Control);
                                |])

    
    //form.Click.Add( fun s -> debug_cursorLocation_Click(form.Cursor.HotSpot))



    Application.Run(form)


    //let addquotes(x : String) = 
    //    x.Insert(0, "\"\"\"")
    //    x.Insert(x.Length-1,"\"\"\"")
   
    //let files2 = files1 |> Array.map(fun s -> "\"\"\"" + s)
    //let files3 = files2 |> Array.map(fun s -> s + "\"\"\"")
                                           
    //let files2 = 
    //        Directory.GetFiles(dir.FullName) 
    //        |>Array.map(fun s -> "\"\"\"" + s)
    //        |>Array.map(fun s -> s + "\"\"\"")
            
        //Array.ForEach(files1, <@ new Action<String>(fun s -> addquotes(s) ) >@ ) //(fun s -> s.Insert(0,""" """))
    
    //let filesFinal = """/play /add """ + String.Join( " ", files3)

    //let p = Path.Combine(song1)


    //initVideoPlaylist(files1)

    //let playlistDirectory = Directory.GetCurrentDirectory() + "\\video_playlist.mpcpl"
    //let fdvs = playlistDirectory.


    //let final = System.IO.File.Exists("D:\Sammy\Classes\2017\Fall\Organization of Programming Languages\Semester_Programming_Project\Semester_Programming_Project\bin\Debug\video_playlist.mpcpl")

    //let f = """ /add /play "D:\Sammy\Classes\2017\Fall\Organization of Programming Languages\Semester_Programming_Project\Semester_Programming_Project\bin\Debug\video_playlist.mpcpl" """

    //let g  = """/play /add""" + f

    //let a = System.Diagnostics.Process.Start("mpc-hc64.exe", "/play \"" +  + "\"")

    //wmplayer.exe
    //"D:\Sammy\musica\para auto\Hamilton\20 - Hamilton - Yorktown (The World Turned Upside Down).mp3" + 
    //", D:\Sammy\musica\para auto\Homestuck\Homestuck - Homestuck Vol. 1-4 - 05 Sburban Countdown.mp3" + 
    //", D:\Sammy\musica\para auto\Old\4chan_city_Craptastrophe.mp3")

 
    0 








