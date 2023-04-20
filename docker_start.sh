docker run --privileged --device /dev/vchiq -v /opt/vc:/opt/vc --env LD_LIBRARY_PATH=/opt/vc/lib -p 80:80/tcp --restart unless-stopped rpidefender
