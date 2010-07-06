MSBUILD_PATH = "C:/Windows/Microsoft.NET/Framework/v4.0.30319/"
BUILD_PATH = File.expand_path('build')
TOOLS_PATH = File.expand_path('tools')
REPORTS_PATH = File.expand_path('reports')
SOLUTION = "src/NCommons.sln"
CONFIG = "Release"

task :default => ["build:all"]

namespace :build do

        task :all => [:compile, :tests]
        
        task :compile do
                sh "#{MSBUILD_PATH}msbuild.exe /p:Configuration=#{CONFIG} /p:OutDir=#{BUILD_PATH}/ #{SOLUTION}"
	end

	task :tests do
		mkdir_p "#{REPORTS_PATH}"
		specs = FileList.new("#{BUILD_PATH}/*.Specs.dll")
		sh "#{TOOLS_PATH}/machine.specifications/mspec.exe -x integration #{specs}"
        end
end
