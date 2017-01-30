import os
import stat
import shutil

SerieDir = "D:\\Downloads\SERIES"
FilmDir = "D:\\Downloads\FILMS"
DLDir = "E:\\Downloads\klaar"
AllowedExtensions = ["srt", "mkv", "mp4", "avi"]

def MkPath(*arg):
	Str = ""
	for x in arg:
		Str = Str + "\\" + str(x)
	
	return Str[1:]

def FindFile(FileName, DirName):
    f = False

    for Root, Directories, Files in os.walk(DirName):
            for File in Files:
                    if (File == FileName):
                            f = True
    return f

def remove_readonly(func, path, excinfo):
    os.chmod(path, stat.S_IWRITE)
    func(path)
	
def DeleteFile(File):
	print("file " + os.path.dirname(File) + " Deleten")
	Found = os.path.isfile(File)
	if(Found):
			os.remove(File)

def DeleteFolder(Folder):
	print("folder " + os.path.dirname(Folder) + " Deleten")
	Found = os.path.isfile(Folder)
	if(Found):
			shutil.rmtree(os.path.dirname(Folder), onerror=remove_readonly)
		
def MoveFile(File, Dir):
	print(File + " Moving File " + Dir)
	FixFile(File)
	Found = os.path.isfile(File)
	if(Found):
			shutil.copy(File, Dir)
			DeleteFile(File)
			
def MoveFolder(Folder, Dir):
	print(Folder + " Moving Folder " + Dir)
	FixFolder(Folder)
	Found = os.path.exists(Folder)
	if(Found):
			shutil.move(Folder, Dir)
			#DeleteFolder(Folder)

def FixFile(File):
		os.chmod(File ,stat.S_IWRITE)
			
def FixFolder(Folder):
	for Root, dirs, files in os.walk(Folder):
			for FolderName in files:
				full_path = os.path.join(Root, FolderName)
				os.chmod(full_path ,stat.S_IWRITE)

def AllowedFile(FileName):
    Allowed = False
    for Extension in AllowedExtensions:
            FileExtension = FileName[-len(Extension):].lower()
            if (FileExtension in AllowedExtensions):
                    Allowed = True

    return Allowed

def FixSeries(File, FileRoot):
    for Root, Directories, Files in os.walk(SerieDir):
			for Dir in Directories:
					if (Dir.lower() in File.lower()):
							NewDir = MkPath(SerieDir, Dir)
							OldFile = MkPath(FileRoot, File)
							Found = FindFile(File, NewDir)
							if (Found):
									DeleteFolder(FileRoot)
							else:
									if (AllowedFile(File) and FileRoot == DLDir):
										MoveFile(OldFile, NewDir)
									else:
										MoveFolder(FileRoot, NewDir)
									
def FixMovies(File, FileRoot):
		if(FileRoot != DLDir and AllowedFile(File)):
			OldFile = MkPath(FileRoot, File)
			Found = FindFile(File, FilmDir)
			if (Found):
					DeleteFolder(OldFile)
			else:
					if (AllowedFile(File)):
						MoveFolder(FileRoot, FilmDir)
					
		else:
			DeleteFile(File)
			
for Root, Directories, Files in os.walk(DLDir):
    for File in Files:
			Found = os.path.exists(Root)
			if(Found):
				FixSeries(File, Root)
				FixMovies(File, Root)
