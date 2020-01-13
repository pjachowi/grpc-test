import asyncio

from grpclib.utils import graceful_exit
from grpclib.server import Server

from greeter_python_grpc_pb import greeter_grpc
from greeter_pb2 import HelloRequest
from greeter_pb2 import HelloReply

class Greeter(greeter_grpc.GreeterBase):

    async def SayHello(self, stream):
        request: greeter_pb2.HelloRequest = await stream.recv_message()
        # request = await stream.recv_message()
        # print(f'Received: {request}')
        message = f'Hello, {request.name}!'
        reply = HelloReply(message=message)
        await stream.send_message(reply)


async def main(*, host='127.0.0.1', port=5001):
    server = Server([Greeter()])
    with graceful_exit([server]):
        await server.start(host, port)
        print(f'Serving on {host}:{port}')
        await server.wait_closed()


if __name__ == '__main__':
    asyncio.run(main())