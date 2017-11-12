open System
open System.Windows.Forms
open System.Drawing
open System.IO
open System.Diagnostics
open System.Collections

//open System.Windows.Forms.CheckedListBox

//open System.Windows.Forms.CheckedListBox

//open System.Media
//open System.Windows.Forms.DataGridViewComboBoxCell

let stopWatch = System.Diagnostics.Stopwatch.StartNew()

// REALLY BAD STUFF BUT I KINDA RUN OUT OF IDEAS SO SUE ME
let spoopyGhostMediaList = new ArrayList()


let debug_window_size_key_press(f : System.Drawing.Size, e : KeyEventArgs) = 
        match e with
        | e when e.KeyCode = Keys.C -> Console.WriteLine("Form Width: {0}\nForm Height: {1}", f.Width, f.Height)
        | e when e.KeyCode = Keys.K -> Console.WriteLine("erwrewrf")
        | _  -> ignore()


// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

let mainFileExit_Click(sender : System.Object, e : EventArgs) = 
        exit 0 |> ignore

//let stuffToDoWhenKeyPressed s (e : KeyEventArgs) = 
let debug_window_size(sender : System.Drawing.Size, lstBox : ListBox) = 
        Console.Clear()
        Console.WriteLine("Form Width: {0}\nForm Height: {1}",sender.Width, sender.Height)
        Console.WriteLine(lstBox.Items)
        //for i in lstBox.Items do
        //   Console.WriteLine(i)

        //match e with
        //| e when Char.ToLower e.KeyChar = 'q' -> Console.WriteLine("Form Width: {0}\nForm Height: {1}",sender.Width, sender.Height)
        //| _ -> ignore()
        
let numberCheck = System.Text.RegularExpressions.Regex("^[0-9]+$")    
let strContainsOnlyNumbers (s:string) = numberCheck.IsMatch s
let swap (a: _[]) x y =
    let tmp = a.[x]
    a.[x] <- a.[y]
    a.[y] <- tmp

//let swap i j =
//        let temp = array.[i]
//        array.[i] <- array.[j]
//        array.[j] <- temp
//let Shuffle2 arr : (FileInfo[] -> string[]) = 
    

let Randomizer (tbxDirectory : TextBox, lstBox: ListBox, size: String) = //,  [<Out>] listOfRandomNumbers : list<int32> byref) = 
    lstBox.Items.Clear()
    // setting up "random"
    stopWatch.Stop()
    let stopwatchElapsedTime = stopWatch.ElapsedMilliseconds.ToString()
    stopWatch.Reset()
    stopWatch.Start()
    let rnd = System.Random(System.Int32.Parse(stopwatchElapsedTime))
    let shuffle a = Array.iteri ( fun i _ -> swap a i (rnd.Next(i, Array.length a)) ) a
    
    match strContainsOnlyNumbers(size) with 
    | true -> 
              let sizeInt = System.Int32.Parse(size)
              let found_files_dir = new DirectoryInfo(tbxDirectory.Text)
              let found_files = found_files_dir.GetFiles() 
              //let f = Array.iter(fun _ -> ) found_files
              shuffle(found_files)
              //let Shuffle2 = found_files |> Seq.toArray()
              for i in found_files do
                  lstBox.Items.Add(i) |>ignore
                  spoopyGhostMediaList.Add(i.FullName) |>ignore
              
    | false -> MessageBox.Show("List Size is Invalid") |> ignore        
    lstBox.SelectedItem <- 0
    

    //fun (combos : string list) -> List.nth combos (rnd.Next(combos.Length))

    //if strContainsOnlyNumbers(size.Text) = true then
    //   let sizeInt = System.Int32.Parse(size.Text)
    //   if sizeInt <= lstBox.Items.Count then
    //       listOfRandomNumbers = List.init sizeInt (fun _ -> rnd.Next(0,10))
    //   else
    //       listOfRandomNumbers = List.init lstBox.Items.Count (fun _ -> rnd.Next(0,10))
    //ignore



let directoryButton_Click (tbxDirectory : TextBox, lstBox: ListBox, size: String, e : EventArgs) =
        let folderBrowser = new FolderBrowserDialog()
        let browserResult = folderBrowser.ShowDialog()
        if browserResult = DialogResult.OK then 
                tbxDirectory.Text <- folderBrowser.SelectedPath
                let found_files_dir = new DirectoryInfo(tbxDirectory.Text)
                let found_files = found_files_dir.GetFiles()
                //Directory.GetFiles(tbtBox.Text)
                //let mutable randomIndices = new list<int32>
                Randomizer(tbxDirectory, lstBox, size) //, &randomIndices)
        //        for i in found_files do
        //            lstBox.Items.Add(i) |>ignore
        //lstBox.SelectedItem <- 0

let randomizeButton_Click(tbxDirectory : TextBox, lstBox: ListBox, size: TextBox, e : EventArgs) =
      Randomizer(tbxDirectory, lstBox, size.Text)
      



let playButton_Click (lstBox: ListBox, e : EventArgs) = 
    if not(lstBox.Items.Count = 0) then
        let arr : obj[] = Array.empty
        //lstBox.Items.CopyTo(arr,0)
        //lstBox.Items
        //Array.init 100 (fun x -> {value1 = "x"; value2 = "y"})
        //let arr = Seq.init lstBox.Items.Count (fun _ -> "")
        let arr = new ArrayList(lstBox.Items)
        //let mutable seeeq = Seq.empty
        //let mutable counter = 0
        //let mutable arr = arrrrr
        
        //for i in  0..arr.Count do
        //   lstBox.SelectedIndex <- i
        //   seeeq <- Seq.append(arr.[i])
           

           //arr <- Set.map(lstBox.SelectedItem.ToString())

         //   arr.[myIter] <- lstBox.Items.Item.[myIter].ToString()//[myIter]
        //let a = Array.ConvertAll(found_files, Converter(string) )
        let f = spoopyGhostMediaList
        let arr = seq { for i in 0..spoopyGhostMediaList.Count -> spoopyGhostMediaList.Item i}
        //let files1 = 
        //    Seq.toArray(arr)
        //    |>Array.map(fun s -> "\"\"\"" + (s.FullName) )
        //    |>Array.map(fun s -> s + "\"\"\"")
        let files1 = arr
        let files2 = files1 |> Seq.map(fun s -> "\"\"\"" + s)
        let files3 = files2 |> Seq.map(fun s -> s + "\"\"\"")

        let filesFinal = """/play /add """ + String.Join( " ", files3)
        System.Diagnostics.Process.Start("""C:\Program Files\MPC-HC\mpc-hc64.exe""", filesFinal) |> ignore // D:\Sammy\musica\\")



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
    let listPanel = new Panel()

    let tbxDirectory = new TextBox()
    let btnDirectory = new Button()
    let btnRandomize = new Button()
    let btnPlay = new Button()
    let cbxMusicBox = new CheckBox()
    let cbxVideoBox = new CheckBox()
    let lblAmount = new Label()
    let tbxAmount = new TextBox()

    let mediaList = new ListBox()

    form.AutoScaleMode <- System.Windows.Forms.AutoScaleMode.Font
    form.Size <- new System.Drawing.Size(1000, 440)
    
    form.MinimumSize <- new System.Drawing.Size(1000, 440)
    form.MaximumSize <- new System.Drawing.Size(1000, 440)
    form.Name <- "Playlist Randomizer"
    form.Text <- "Playlist Randomizer"
    form.BackColor <- Color.Red
    // main menu
 
    mainFileExit.Click.AddHandler(new System.EventHandler (fun s e -> mainFileExit_Click(s, e)))
    mainFile.MenuItems.AddRange( [| mainFileExit |] )
    mainMenu.MenuItems.AddRange( [| mainFile |] )
    form.Menu <- mainMenu

    

    buttonPanel.Name <- "buttonPanel"
    buttonPanel.TabIndex <- 0
    buttonPanel.Dock <- System.Windows.Forms.DockStyle.Left
    buttonPanel.Size <- new System.Drawing.Size(300, 440) // x = 520
    //buttonPanel.Location <- new System.Drawing.Point(0, 0)
    buttonPanel.BackColor <- Color.Blue
    //buttonPanel.Width <- form.Width/2 // trying to figure out size

    listPanel.Dock <- System.Windows.Forms.DockStyle.Right;
    //listPanel.Location <- new System.Drawing.Point(100, 0);
    listPanel.Name <- "ListPanel";
    listPanel.Size <- new System.Drawing.Size(form.Width-buttonPanel.Width-16, 440);
    listPanel.TabIndex <- 1;

    // Directory Textbox
    tbxDirectory.Location <- new System.Drawing.Point(26, 35)
    tbxDirectory.Name <- "tbxDirectory"
    tbxDirectory.Size <- new System.Drawing.Size(250, 31)
    tbxDirectory.TabIndex <- 0
    tbxDirectory.Text <- "D:\\Sammy\\anime"

    // directory button
    btnDirectory.Text <- "Directory"
    btnDirectory.Location <- new System.Drawing.Point(90, 80)
    tbxDirectory.Name <- "tbxDirectory"
    btnDirectory.Size <- new System.Drawing.Size(120, 30)
    btnDirectory.TabIndex <- 1 //??
    btnDirectory.Click.AddHandler(new System.EventHandler (fun _ e -> directoryButton_Click(tbxDirectory, mediaList, tbxAmount.Text, e))) // change to the real function

    
    // Randomize
    btnRandomize.Location <- new System.Drawing.Point(90, 130)
    btnRandomize.Name <- "btnRandomize"
    btnRandomize.Size <- new System.Drawing.Size(120, 30)
    btnRandomize.Text <- "Randomize"
    btnRandomize.UseVisualStyleBackColor <- true
    btnRandomize.TabIndex <- 2
    btnRandomize.Click.AddHandler(new System.EventHandler (fun _ e -> randomizeButton_Click(tbxDirectory,mediaList, tbxAmount, e))) // change to the real function


    // Play Button
    btnPlay.Location <- new System.Drawing.Point(90, 180)
    btnPlay.Name <- "btnPlay";
    btnPlay.Size <- new System.Drawing.Size(120, 30)
    btnPlay.Text <- "Play";
    btnPlay.UseVisualStyleBackColor <- true;
    btnPlay.TabIndex <- 3;
    btnPlay.Click.AddHandler(new System.EventHandler (fun _ e -> playButton_Click(mediaList, e))) // change to the real function

    //// debug button
    let debugButton = new Button()
    debugButton.Text <- "Debug"
    debugButton.Click.AddHandler(new System.EventHandler (fun _ _ -> debug_window_size (form.Size,mediaList))) // change to the real function

    //form.KeyPress.AddHandler(new KeyPressEventHandler (fun f e -> debug_window_size_key_press(form.Size, e)))
    //form.KeyDown.AddHandler(new System.Windows.Forms.KeyEventHandler (fun s e -> debug_window_size_key_press(form.Size, e)))

    // videoBox check box
    cbxVideoBox.Location <- new System.Drawing.Point(200, 227)
    cbxVideoBox.Name <- "chbxVideoBox"
    cbxVideoBox.Size <- new System.Drawing.Size(100, 30)
    cbxVideoBox.Text <- "Video"
    cbxVideoBox.AutoSize <- true
    cbxVideoBox.TabIndex <- 4

    // musicBox check box
    cbxMusicBox.Location <- new System.Drawing.Point(200, 240)
    cbxMusicBox.Name <- "chbxMusicBox"
    cbxMusicBox.Size <- new System.Drawing.Size(100, 30)
    cbxMusicBox.Text <- "Music"
    cbxMusicBox.TabIndex <- 5

    // size of list label
    lblAmount.Text <- "Size of List"
    lblAmount.TextAlign <- ContentAlignment.TopCenter
    lblAmount.Size <- new System.Drawing.Size(50,50)
    lblAmount.Location <- new System.Drawing.Point(20,230)
    lblAmount.TabIndex <- 6

    // size of list box
    tbxAmount.Name <- "tbxAmount"
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
    buttonPanel.Controls.Add(debugButton)

    buttonPanel.ResumeLayout(false)
    buttonPanel.PerformLayout()
    form.ResumeLayout(false)
    form.AutoScaleMode <- System.Windows.Forms.AutoScaleMode.Font


   // media list box
    mediaList.Name <- "MediaList"
    mediaList.Dock <- System.Windows.Forms.DockStyle.Fill
    mediaList.FormattingEnabled <- true
    mediaList.ItemHeight <- 25
    mediaList.Location <- new System.Drawing.Point(538, 0)
    mediaList.Name <- "MediaList";
    mediaList.Size <- new System.Drawing.Size(592, 729)
    mediaList.TabIndex <- 1

    listPanel.Controls.Add(mediaList)
    
    // Background
    let Background = new Panel()
    Background.Anchor <- (System.Windows.Forms.AnchorStyles.Top ||| System.Windows.Forms.AnchorStyles.Bottom ||| (System.Windows.Forms.AnchorStyles.Left ||| System.Windows.Forms.AnchorStyles.Right))
    
    Background.Dock <- System.Windows.Forms.DockStyle.Fill;
    Background.ForeColor <- System.Drawing.SystemColors.ControlText;
    Background.Location <- new System.Drawing.Point(0, 0);
    Background.Name <- "Background";
    Background.Size <- new System.Drawing.Size(2035, 1198);
    Background.TabIndex <- 0;

    Background.Controls.AddRange([| 
                                    (buttonPanel)
                                    (listPanel)
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








