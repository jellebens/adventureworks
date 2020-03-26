workflow PingUrlParallel {

  param(
    [string]$url,
    [int]$parallelCount = 50,
    [int]$iterations = 10000
  )

  foreach -parallel ($x in 1..$parallelCount) {
    1..$iterations | %{ 
        $response = curl $url
        $status = $response.StatusCode
        "worker $x : iteration $_ : $status"
        [System.Threading.Thread]::Sleep(500)
    }
  }
}

PingUrlParallel http://localhost/Vendors/Vendors