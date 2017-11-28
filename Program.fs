open System
open System.Windows.Forms
open System.Drawing
open System.IO
open System.Diagnostics
open System.Collections

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

let stopWatch = System.Diagnostics.Stopwatch.StartNew()
// I THINK THIS IS REALLY BAD STUFF BUT I KINDA RUN OUT OF IDEAS SO SUE ME
let spoopyGhostMediaList = new ArrayList()

let debug_window_size_key_press(f : System.Drawing.Size, e : KeyEventArgs) = 
        match e with
        | e when e.KeyCode = Keys.C -> Console.WriteLine("Form Width: {0}\nForm Height: {1}", f.Width, f.Height)
        | e when e.KeyCode = Keys.K -> Console.WriteLine("erwrewrf")
        | _  -> ignore()




let mainFileExit_Click(sender : System.Object, e : EventArgs) = 
        exit 0 |> ignore

//let stuffToDoWhenKeyPressed s (e : KeyEventArgs) = 
let debug_window_size(sender : System.Drawing.Size, lstBox : ListBox) = 
        Console.Clear()
        Console.WriteLine("Form Width: {0}\nForm Height: {1}",sender.Width, sender.Height)
        Console.WriteLine(lstBox.Items)

  
// TODO: not gonna lie, I didnt do these 3
// taken from here: https://stackoverflow.com/questions/42253284/f-check-if-a-string-contains-only-number
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
    

let addToList(lstBox : ListBox, toAdd : FileInfo[], size : int) = 
    // for loops are bad but I am kinda out of ideas
    for i in 0..size do
        lstBox.Items.Add( toAdd.[i]) |>ignore
        spoopyGhostMediaList.Add(toAdd.[i].FullName) |>ignore

let ShuffleRuffleAndAdd(lstBox: ListBox, sizeInt : int, filtered_files :FileInfo[]) = 
    // setting up "random"
    stopWatch.Stop()
    let stopwatchElapsedTime = stopWatch.ElapsedMilliseconds.ToString()
    stopWatch.Reset()
    stopWatch.Start()
    let rnd = System.Random(System.Int32.Parse(stopwatchElapsedTime))
    let shuffle a = Array.iteri ( fun i _ -> swap a i (rnd.Next(i, Array.length a)) ) a

    // shuffle
    shuffle(filtered_files)

    // ruffle
    match sizeInt with
       | sizeInt when sizeInt <= filtered_files.Length -> addToList(lstBox, filtered_files, sizeInt-1)
       | _ -> addToList(lstBox, filtered_files, filtered_files.Length-1)



let Randomizer (tbxDirectory : TextBox, lstBox: ListBox, size: String, videoBox : CheckBox) = //,  [<Out>] listOfRandomNumbers : list<int32> byref) = 
    lstBox.Items.Clear()
    spoopyGhostMediaList.Clear()
    //if not(String.IsNullOrEmpty(tbxDirectory.Text)) then
    match String.IsNullOrEmpty(tbxDirectory.Text) with 
    | false ->   
             match strContainsOnlyNumbers(size) with 
             | true -> 
                       let sizeInt = System.Int32.Parse(size)
                       let found_files_dir = new DirectoryInfo(tbxDirectory.Text)
                       let found_files = found_files_dir.GetFiles() 
                       //let f = Array.iter(fun _ -> ) found_files
                       match videoBox.Checked with
                       |true ->
                             // if we want videos
                             let filtered_files = 
                                found_files 
                                |> Array.filter(fun s -> 
                                                let strEnd = (String.length s.Name) - 3
                                                not(s.Name.Substring strEnd = "mp3"))
                             ShuffleRuffleAndAdd(lstBox,sizeInt,filtered_files)
                        |false -> 
                             // if we want music
                             let filtered_files = 
                                found_files 
                                |> Array.filter(fun s ->
                                                let strEnd = (String.length s.Name) - 3
                                                s.Name.Substring strEnd = "mp3")
                             ShuffleRuffleAndAdd(lstBox,sizeInt,filtered_files)
             | false -> MessageBox.Show("List Size is Invalid") |> ignore    
    | true -> ignore()
    lstBox.SelectedItem <- 0
    

let directoryButton_Click (tbxDirectory : TextBox, lstBox: ListBox, size: String, videoBox : CheckBox, e : EventArgs) =
        let folderBrowser = new FolderBrowserDialog()
        let browserResult = folderBrowser.ShowDialog()
        //if browserResult = DialogResult.OK then 
        match browserResult with
        | DialogResult.OK ->
                                tbxDirectory.Text <- folderBrowser.SelectedPath
                                //let found_files_dir = new DirectoryInfo(tbxDirectory.Text)
                                //let found_files = found_files_dir.GetFiles()
                                Randomizer(tbxDirectory, lstBox, size,videoBox) //, &randomIndices)
        | _ -> ignore() 

let randomizeButton_Click(tbxDirectory : TextBox, lstBox: ListBox, size: TextBox, videoBox : CheckBox, e : EventArgs) =
      Randomizer(tbxDirectory, lstBox, size.Text,videoBox)
      
      
let playButton_Click (lstBox: ListBox, e : EventArgs) = 
    match lstBox.Items.Count with
    | 0 -> ignore() // no files detected
    | _ ->
            let files = 
                spoopyGhostMediaList.ToArray()
                |> Array.map(fun s -> "\"\"\"" + (s.ToString())) // add " at the beggining of every file name
                |> Array.map(fun s -> s + "\"\"\"") // add " at the end of every file name

            let filesFinal = """/play /add """ + String.Join( " ", files)
            System.Diagnostics.Process.Start("""C:\Program Files\MPC-HC\mpc-hc64.exe""", filesFinal) |> ignore // D:\Sammy\musica\\")
   

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
    //form.BackColor <- Color.Red
    
    // main menu
    mainFileExit.Click.AddHandler(new System.EventHandler (fun s e -> mainFileExit_Click(s, e)))
    mainFile.MenuItems.AddRange( [| mainFileExit |] )
    mainMenu.MenuItems.AddRange( [| mainFile |] )
    form.Menu <- mainMenu

    

    buttonPanel.Name <- "buttonPanel"
    buttonPanel.TabIndex <- 0
    buttonPanel.Dock <- System.Windows.Forms.DockStyle.Left
    buttonPanel.Size <- new System.Drawing.Size(300, 440) // x = 520

    listPanel.Dock <- System.Windows.Forms.DockStyle.Right;
    listPanel.Name <- "ListPanel";
    listPanel.Size <- new System.Drawing.Size(form.Width-buttonPanel.Width-16, 440);
    listPanel.TabIndex <- 1;

    // Directory Textbox
    tbxDirectory.Location <- new System.Drawing.Point(26, 35)
    tbxDirectory.Name <- "tbxDirectory"
    tbxDirectory.Size <- new System.Drawing.Size(250, 31)
    tbxDirectory.TabIndex <- 0

    // directory button
    btnDirectory.Text <- "Directory"
    btnDirectory.Location <- new System.Drawing.Point(90, 80)
    tbxDirectory.Name <- "tbxDirectory"
    btnDirectory.Size <- new System.Drawing.Size(120, 30)
    btnDirectory.TabIndex <- 1 //??
    btnDirectory.Click.AddHandler(new System.EventHandler (fun _ e -> directoryButton_Click(tbxDirectory, mediaList, tbxAmount.Text,cbxVideoBox, e))) // change to the real function

    
    // Randomize
    btnRandomize.Location <- new System.Drawing.Point(90, 130)
    btnRandomize.Name <- "btnRandomize"
    btnRandomize.Size <- new System.Drawing.Size(120, 30)
    btnRandomize.Text <- "Randomize"
    btnRandomize.UseVisualStyleBackColor <- true
    btnRandomize.TabIndex <- 2
    btnRandomize.Click.AddHandler(new System.EventHandler (fun _ e -> randomizeButton_Click(tbxDirectory,mediaList, tbxAmount,cbxVideoBox, e))) // change to the real function


    // Play Button
    btnPlay.Location <- new System.Drawing.Point(90, 180)
    btnPlay.Name <- "btnPlay";
    btnPlay.Size <- new System.Drawing.Size(120, 30)
    btnPlay.Text <- "Play";
    btnPlay.UseVisualStyleBackColor <- true;
    btnPlay.TabIndex <- 3;
    btnPlay.Click.AddHandler(new System.EventHandler (fun _ e -> playButton_Click(mediaList, e))) // change to the real function

    //// debug button
    //let debugButton = new Button()
    //debugButton.Text <- "Debug"
    //debugButton.Click.AddHandler(new System.EventHandler (fun _ _ -> debug_window_size (form.Size,mediaList))) // change to the real function

    //form.KeyPress.AddHandler(new KeyPressEventHandler (fun f e -> debug_window_size_key_press(form.Size, e)))
    //form.KeyDown.AddHandler(new System.Windows.Forms.KeyEventHandler (fun s e -> debug_window_size_key_press(form.Size, e)))

    // videoBox check box
    cbxVideoBox.Location <- new System.Drawing.Point(200, 227)
    cbxVideoBox.Name <- "chbxVideoBox"
    cbxVideoBox.Size <- new System.Drawing.Size(100, 30)
    cbxVideoBox.Text <- "Video"
    cbxVideoBox.AutoSize <- true
    cbxVideoBox.TabIndex <- 4
    cbxVideoBox.Checked <- true
    cbxVideoBox.Click.Add(fun r1msg->  
                                    cbxVideoBox.Checked<-true  
                                    cbxMusicBox.Checked<-false)  

    // musicBox check box
    cbxMusicBox.Location <- new System.Drawing.Point(200, 240)
    cbxMusicBox.Name <- "chbxMusicBox"
    cbxMusicBox.Size <- new System.Drawing.Size(100, 30)
    cbxMusicBox.Text <- "Music"
    cbxMusicBox.TabIndex <- 5
    cbxMusicBox.Click.Add(fun r1msg->  
                                    cbxMusicBox.Checked<-true  
                                    cbxVideoBox.Checked<-false)  

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
    //buttonPanel.Controls.Add(debugButton)

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
    0 








